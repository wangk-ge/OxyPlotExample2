using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OxyPlotExample2
{
    public partial class Form1 : Form
    {
        private ConcurrentQueue<double> m_dataQueue = new ConcurrentQueue<double>();
        private FlowSensor m_flowSensor = new FlowSensor();
        private KalmanFilter m_kalmanFilter = new KalmanFilter(0.01f/*Q*/, 0.01f/*R*/, 1.0f/*P*/, 0);
        private LinearAxis m_xAxis; // X轴
        private LinearAxis m_yAxis; // Y轴
        private long m_X = 0;
        private Timer m_refreshTimer = new Timer();
        private readonly int m_fps = 24; // 帧率
        /* 统计数据 */
        private double m_minVal = double.MaxValue; // 最小值
        private double m_maxVal = double.MinValue; // 最大值
        private double m_sumVal = 0; // 求和值
        private double m_minFilterVal = double.MaxValue; // 滤波后最小值
        private double m_maxFilterVal = double.MinValue; // 滤波后最大值
        private double m_sumFilterVal = 0; // 滤波后求和值

        private PlotModel m_plotModel;
        private LineAnnotation m_cursor1Annotation; // 游标1
        private LineAnnotation m_cursor2Annotation; // 游标2

        public Form1()
        {
            InitializeComponent();
        }

        private void EnumSerialPorts()
        {
            toolStripComboBoxCom.Items.Clear();

            SerialPort _tempPort;
            String[] Portname = SerialPort.GetPortNames();

            //create a loop for each string in SerialPort.GetPortNames
            foreach (string str in Portname)
            {
                try
                {
                    _tempPort = new SerialPort(str);
                    _tempPort.Open();

                    //if the port exist and we can open it
                    if (_tempPort.IsOpen)
                    {
                        toolStripComboBoxCom.Items.Add(str);
                        _tempPort.Close();
                    }
                }
                //else we have no ports or can't open them display the 
                //precise error of why we either don't have ports or can't open them
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString(), "Error - No Ports available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex);
                }
            }

            toolStripComboBoxCom.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnumSerialPorts();

            // 定义Model
            m_plotModel = new PlotModel()
            {
                Title = "波形",
                LegendTitle = "图例",
                LegendOrientation = LegendOrientation.Horizontal,
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.TopRight,
                LegendBackground = OxyColors.Beige,
                LegendBorder = OxyColors.Black
            };

            //X轴
            m_xAxis = new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IsZoomEnabled = true,
                IsPanEnabled = true,
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = 1000
            };
            m_plotModel.Axes.Add(m_xAxis);

            //Y轴
            m_yAxis = new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IsZoomEnabled = true,
                IsPanEnabled = true,
                Position = AxisPosition.Left
            };
            m_plotModel.Axes.Add(m_yAxis);

            // 原始数据
            var series = new LineSeries()
            {
                Color = OxyColors.Green,
                StrokeThickness = 1,
                MarkerSize = 1,
                MarkerStroke = OxyColors.DarkGreen,
                MarkerType = MarkerType.Circle,
                Title = "Data"
            };
            m_plotModel.Series.Add(series);

            // 滤波数据
            series = new LineSeries()
            {
                Color = OxyColors.Red,
                StrokeThickness = 1,
                MarkerSize = 1,
                MarkerStroke = OxyColors.DarkRed,
                MarkerType = MarkerType.Circle,
                Title = "Filter"
            };
            m_plotModel.Series.Add(series);

            /* 游标1 */
            m_cursor1Annotation = new LineAnnotation()
            {
                Color = OxyColors.Red,
                X = 0,
                LineStyle = LineStyle.Solid,
                Type = LineAnnotationType.Vertical,
                Text = "1"
            };
            m_plotModel.Annotations.Add(m_cursor1Annotation);

            /* 游标2 */
            m_cursor2Annotation = new LineAnnotation()
            {
                Color = OxyColors.Red,
                X = 0,
                LineStyle = LineStyle.Solid,
                Type = LineAnnotationType.Vertical,
                Text = "2"
            };
            m_plotModel.Annotations.Add(m_cursor2Annotation);

            plotView1.Model = m_plotModel;

            numericUpDownCursor1.Value = (decimal)m_cursor1Annotation.X;
            numericUpDownCursor2.Value = (decimal)m_cursor2Annotation.X;
            numericUpDownCursor1.Maximum = decimal.MaxValue;
            numericUpDownCursor2.Maximum = decimal.MaxValue;
#if false
            var commandLeft = new DelegatePlotCommand<OxyMouseDownEventArgs>((v, c, a) =>
            {
                a.Handled = true;
                if (v.ActualModel.Series.Count > 0)
                {
                    var series2 = v.ActualModel.Series[0] as LineSeries;
                    var point = series2.InverseTransform(a.Position);
                    if (m_plotModel.Annotations.Count > 0 && m_plotModel.Annotations[0] is LineAnnotation)
                    {
                        var lineAnnotation = m_plotModel.Annotations[0] as LineAnnotation;
                        lineAnnotation.X = point.X;
                        lineAnnotation.Text = point.Y.ToString("G3");
                        m_plotModel.InvalidatePlot(false);
                    }
                }
            });
            plotView1.ActualController.BindMouseDown(OxyMouseButton.Left, commandLeft);
#endif

            /* 通过传感器获取数据 */
            m_flowSensor.m_frameDecoder.WaveDataRespRecved += new FrameDecoder.WaveDataRecvHandler((byte channel, double value) => {
                //Console.WriteLine($"WaveDataRespRecved: {channel} {value}");

                m_dataQueue.Enqueue(value);
            });

            m_refreshTimer.Interval = 1000 / m_fps;
            m_refreshTimer.Tick += new EventHandler((timer, arg) => {

                long xBegin = m_X;
                while (m_dataQueue.Count > 0)
                {
                    bool bRet = m_dataQueue.TryDequeue(out double val);
                    if (!bRet)
                    {
                        break;
                    }
                    AddPoint(val);
                }

                long xDelta = m_X - xBegin;
                if (xDelta > 0)
                {
                    m_plotModel.InvalidatePlot(true);

                    var xAxis = m_plotModel.Axes[0];
                    if (checkBoxAutoScroll.Checked && (m_X > xAxis.Maximum))
                    {
                        double panStep = xAxis.Transform(-xDelta + xAxis.Offset);
                        xAxis.Pan(panStep);
                    }
                }
            });

            m_kalmanFilter.R = (float)decimal.ToDouble(numericUpDownR.Value);
        }

        /* 重置统计数据 */
        private void ResetStatisticalData()
        {
            /* 清除数据 */
            m_minVal = double.MaxValue; // 最小值
            m_maxVal = double.MinValue; // 最大值
            m_sumVal = 0; // 求和值
            m_minFilterVal = double.MaxValue; // 滤波后最小值
            m_maxFilterVal = double.MinValue; // 滤波后最大值
            m_sumFilterVal = 0; // 滤波后求和值

            /* 清除显示 */
            this.BeginInvoke(new Action<Form1>((obj) => {
                /* 样本数 */
                textBoxNum.Text = string.Empty;
                textBoxFilterNum.Text = string.Empty;
                /* 求和值 */
                textBoxSum.Text = string.Empty;
                textBoxFilterSum.Text = string.Empty;
                /* 平均值 */
                textBoxAvg.Text = string.Empty;
                textBoxFilterAvg.Text = string.Empty;
                /* 最小值 */
                textBoxMin.Text = string.Empty;
                textBoxFilterMin.Text = string.Empty;
                /* 最大值 */
                textBoxMax.Text = string.Empty;
                textBoxFilterMax.Text = string.Empty;
                /* 波动范围 */
                textBoxRange.Text = string.Empty;
                textBoxFilterRange.Text = string.Empty;
                /* 游标自动选择数据范围 */
                CursorAutoSelect();
            }), this);
        }

        /* 更新统计数据 */
        private void UpdateStatisticalData(double val, double filterVal, long num)
        {
            /* 求和值 */
            m_sumVal += val;
            m_sumFilterVal += filterVal;

            /* 求平均值 */
            double avgVal = m_sumVal / num;
            double avgFilterVal = m_sumFilterVal / num;

            /* 统计最小值 */
            if (val < m_minVal)
            {
                m_minVal = val;
            }
            if (filterVal < m_minFilterVal)
            {
                m_minFilterVal = filterVal;
            }
            /* 统计最大值 */
            if (val > m_maxVal)
            {
                m_maxVal = val;
            }
            if (filterVal > m_maxFilterVal)
            {
                m_maxFilterVal = filterVal;
            }

            /* 计算波动范围 */
            double rangeVal = m_maxVal - m_minVal;
            double rangeFilterVal = m_maxFilterVal - m_minFilterVal;

            /* 更新显示 */
            this.BeginInvoke(new Action<Form1>((obj) => {
                /* 样本数 */
                textBoxNum.Text = num.ToString();
                textBoxFilterNum.Text = num.ToString();
                /* 求和值 */
                textBoxSum.Text = m_sumVal.ToString();
                textBoxFilterSum.Text = m_sumFilterVal.ToString();
                /* 平均值 */
                textBoxAvg.Text = avgVal.ToString();
                textBoxFilterAvg.Text = avgFilterVal.ToString();
                /* 最小值 */
                textBoxMin.Text = m_minVal.ToString();
                textBoxFilterMin.Text = m_minFilterVal.ToString();
                /* 最大值 */
                textBoxMax.Text = m_maxVal.ToString();
                textBoxFilterMax.Text = m_maxFilterVal.ToString();
                /* 波动范围 */
                textBoxRange.Text = rangeVal.ToString();
                textBoxFilterRange.Text = rangeFilterVal.ToString();
                /* 游标自动选择数据范围 */
                CursorAutoSelect();
            }), this);
        }

        private void ClearAll()
        {
            var lineSer1 = plotView1.Model.Series[0] as LineSeries;
            lineSer1.Points.Clear();

            var lineSer2 = plotView1.Model.Series[1] as LineSeries;
            lineSer2.Points.Clear();

            m_X = 0;

            /* 尝试清空队列 */
            while (m_dataQueue.Count > 0)
            {
                bool bRet = m_dataQueue.TryDequeue(out _);
                if (!bRet)
                {
                    break;
                }
            }

            var xAxis = m_plotModel.Axes[0];
            xAxis.Reset();

            /* 重置统计数据 */
            ResetStatisticalData();

            m_plotModel.InvalidatePlot(true);
        }

        private void AddPoint(double val)
        {
            var lineSer1 = plotView1.Model.Series[0] as LineSeries;
            lineSer1.Points.Add(new DataPoint(m_X, val));

            double filterVal = m_kalmanFilter.Input((float)val); // 执行滤波
            var lineSer2 = plotView1.Model.Series[1] as LineSeries;
            lineSer2.Points.Add(new DataPoint(m_X, filterVal));

            m_X++;

            if (checkBoxAutoCursor.Checked)
            { // 已配置游标自动选择功能
                /* 更新统计数据 */
                UpdateStatisticalData(val, filterVal, lineSer1.Points.Count);
            }
        }

        private void ApplyFilter()
        {
            m_kalmanFilter.R = (float)decimal.ToDouble(numericUpDownR.Value);

            var lineSer1 = plotView1.Model.Series[0] as LineSeries;

            var lineSer2 = plotView1.Model.Series[1] as LineSeries;
            lineSer2.Points.Clear();

            for (int i = 0; i < lineSer1.Points.Count; ++i)
            {
                lineSer2.Points.Add(new DataPoint(i, m_kalmanFilter.Input((float)lineSer1.Points[i].Y)));
            }

            m_plotModel.InvalidatePlot(true);
        }

        private async void SendCmd(string cmd)
        {
            if (!m_flowSensor.IsOpen())
            {
                return;
            }

            this.textBoxInfo.AppendText($"Sned: {cmd} \r\n");
            string cmdResp = await m_flowSensor.ExcuteCmdAsync(cmd, 2000);
            this.textBoxInfo.AppendText($"Revc: {cmdResp} \r\n");
        }

        /* 游标自动选择数据范围 */
        private void CursorAutoSelect()
        {
            m_cursor1Annotation.X = 0;
            m_cursor2Annotation.X = m_X;
            m_plotModel.InvalidatePlot(false);

            numericUpDownCursor1.Value = (decimal)m_cursor1Annotation.X;
            numericUpDownCursor2.Value = (decimal)m_cursor2Annotation.X;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendCmd(this.textBoxCmd.Text);
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if ("连接" == toolStripButtonOpen.Text)
            {
                bool bRet = m_flowSensor.Open(toolStripComboBoxCom.Text);
                toolStripButtonStart.Enabled = bRet;
                //toolStripButtonZero.Enabled = bRet;
                //toolStripButtonLoad.Enabled = !bRet;
                //toolStripButtonSave.Enabled = !bRet;
                //toolStripButtonClear.Enabled = !bRet;
                toolStripButtonRefresh.Enabled = !bRet;
                toolStripComboBoxCom.Enabled = !bRet;
                toolStripButtonOpen.Text = bRet ? "断开" : "连接";
            }
            else
            {
                m_flowSensor.Close();
                toolStripButtonStart.Enabled = false;
                toolStripButtonZero.Enabled = false;
                toolStripButtonLoad.Enabled = true;
                toolStripButtonSave.Enabled = true;
                toolStripButtonClear.Enabled = true;
                toolStripButtonRefresh.Enabled = true;
                toolStripComboBoxCom.Enabled = true;
                buttonFilterApply.Enabled = true;
                //numericUpDownR.Enabled = true;
                toolStripButtonOpen.Text = "连接";
                toolStripButtonStart.Text = "开始";
                /* 停止刷新定时器 */
                m_refreshTimer.Stop();
            }
        }

        private async void toolStripButtonStart_Click(object sender, EventArgs e)
        {
            string cmd = "[ADC_START]";
            if ("停止" == toolStripButtonStart.Text)
            {
                cmd = "[ADC_STOP]";
            }
            this.textBoxInfo.AppendText($"Sned: {cmd} \r\n");
            string cmdResp = await m_flowSensor.ExcuteCmdAsync(cmd, 2000);
            this.textBoxInfo.AppendText($"Revc: {cmdResp} \r\n");

            if ("[OK]" == cmdResp)
            {
                if ("开始" == toolStripButtonStart.Text)
                {
                    toolStripButtonStart.Text = "停止";
                    toolStripButtonZero.Enabled = true;
                    buttonFilterApply.Enabled = false;
                    //numericUpDownR.Enabled = false;
                    toolStripButtonClear.Enabled = false;
                    toolStripButtonLoad.Enabled = false;
                    toolStripButtonSave.Enabled = false;
                    //ClearAll();
                    /* 启动刷新定时器 */
                    m_refreshTimer.Start();
                }
                else // if ("停止" == toolStripButtonStart.Text)
                {
                    toolStripButtonStart.Text = "开始";
                    toolStripButtonZero.Enabled = false;
                    buttonFilterApply.Enabled = true;
                    //numericUpDownR.Enabled = true;
                    toolStripButtonClear.Enabled = true;
                    toolStripButtonLoad.Enabled = true;
                    toolStripButtonSave.Enabled = true;
                    /* 停止刷新定时器 */
                    m_refreshTimer.Stop();
                }
            }
        }

        private void toolStripButtonZero_Click(object sender, EventArgs e)
        {
            SendCmd("[ADC_CAL]");
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveCSVDialog = new SaveFileDialog();
            saveCSVDialog.Filter = "CSV File (*.csv;)|*.csv";
            //saveCSVDialog.Multiselect = false;

            if (saveCSVDialog.ShowDialog() == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(saveCSVDialog.FileName))
                {
                    return;
                }

                toolStripButtonSave.Enabled = false;

                Task.Factory.StartNew(() =>
                {
                    var lineSer1 = plotView1.Model.Series[0] as LineSeries;
                    StringBuilder strData = new StringBuilder();
                    foreach (var point in lineSer1.Points)
                    {
                        strData.Append(point.Y);
                        strData.Append(",");
                    }

                    using (StreamWriter writer = new StreamWriter(saveCSVDialog.FileName, false, Encoding.UTF8))
                    {
                        writer.Write(strData);
                        writer.Close();

                        MessageBox.Show("保存成功.");
                    }

                    this.BeginInvoke(new Action<Form1>((obj) => { toolStripButtonSave.Enabled = true; }), this);
                });
            }
        }

        private void toolStripButtonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openCSVDialog = new OpenFileDialog();
            openCSVDialog.Filter = "CSV File (*.csv;)|*.csv";
            openCSVDialog.Multiselect = false;

            if (openCSVDialog.ShowDialog() == DialogResult.OK)
            {
                if (String.IsNullOrEmpty(openCSVDialog.FileName))
                {
                    return;
                }

                toolStripButtonLoad.Enabled = false;

                Task.Factory.StartNew(() =>
                {
                    string strData = String.Empty;

                    using (StreamReader reader = new StreamReader(openCSVDialog.FileName, Encoding.UTF8))
                    {
                        strData = reader.ReadToEnd();
                        reader.Close();
                    }

                    ClearAll();

                    string[] strDataArray = strData.Split(new char[] { ',' });
                    foreach (var strVal in strDataArray)
                    {
                        if (String.Empty == strVal)
                        {
                            continue;
                        }
                        double val = Convert.ToDouble(strVal);

                        AddPoint(val);
                    }

                    this.BeginInvoke(new Action<Form1>((obj) => { toolStripButtonLoad.Enabled = true; }), this);

                    m_plotModel.InvalidatePlot(true);
                });
            }
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void numericUpDownR_ValueChanged(object sender, EventArgs e)
        {
            m_kalmanFilter.R = (float)decimal.ToDouble(numericUpDownR.Value);
        }

        private void buttonFilterApply_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            EnumSerialPorts();
        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            this.textBoxInfo.Clear();
        }

        private void textBoxCmd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendCmd(this.textBoxCmd.Text);
            }
        }

        private void buttonResetScale_Click(object sender, EventArgs e)
        {
            m_plotModel.ResetAllAxes();
            m_plotModel.InvalidatePlot(false);
        }

        private void numericUpDownCursor1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCursor1.Value != (decimal)m_cursor1Annotation.X)
            {
                m_cursor1Annotation.X = (double)numericUpDownCursor1.Value;
                m_plotModel.InvalidatePlot(false);
            }
        }

        private void numericUpDownCursor2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCursor2.Value != (decimal)m_cursor2Annotation.X)
            {
                m_cursor2Annotation.X = (double)numericUpDownCursor2.Value;
                m_plotModel.InvalidatePlot(false);
            }
        }

        private void buttonCursorSelectAll_Click(object sender, EventArgs e)
        {
            /* 游标自动选择数据范围 */
            CursorAutoSelect();
        }

        private void buttonStatistical_Click(object sender, EventArgs e)
        {
            var dataSer = plotView1.Model.Series[0] as LineSeries;
            var dataPoints = dataSer.Points;

            if (0 == dataPoints.Count)
            {
                return;
            }

            int cursorStart;
            int cursorEnd;
            if (m_cursor1Annotation.X > m_cursor2Annotation.X)
            {
                cursorStart = (int)m_cursor2Annotation.X;
                cursorEnd = (int)m_cursor1Annotation.X;
            }
            else //if (m_cursor1Annotation.X <= m_cursor2Annotation.X)
            {
                cursorStart = (int)m_cursor1Annotation.X;
                cursorEnd = (int)m_cursor2Annotation.X;
            }

            if (cursorStart >= dataPoints.Count)
            {
                /* 样本数 */
                textBoxNum.Text = string.Empty;
                textBoxFilterNum.Text = string.Empty;
                /* 求和值 */
                textBoxSum.Text = string.Empty;
                textBoxFilterSum.Text = string.Empty;
                /* 平均值 */
                textBoxAvg.Text = string.Empty;
                textBoxFilterAvg.Text = string.Empty;
                /* 最小值 */
                textBoxMin.Text = string.Empty;
                textBoxFilterMin.Text = string.Empty;
                /* 最大值 */
                textBoxMax.Text = string.Empty;
                textBoxFilterMax.Text = string.Empty;
                /* 波动范围 */
                textBoxRange.Text = string.Empty;
                textBoxFilterRange.Text = string.Empty;
                return;
            }

            cursorEnd = (cursorEnd < dataPoints.Count) ? cursorEnd : (dataPoints.Count - 1);

            var filterSer = plotView1.Model.Series[1] as LineSeries;
            var filterPoints = filterSer.Points;

            double minVal = double.MaxValue; // 最小值
            double maxVal = double.MinValue; // 最大值
            double sumVal = 0; // 求和值
            double minFilterVal = double.MaxValue; // 滤波后最小值
            double maxFilterVal = double.MinValue; // 滤波后最大值
            double sumFilterVal = 0; // 滤波后求和值
            for (int i = cursorStart; i <= cursorEnd; ++i)
            {
                var val = dataPoints[i].Y;
                var filterVal = filterPoints[i].Y;

                /* 求和值 */
                sumVal += val;
                sumFilterVal += filterVal;

                /* 统计最小值 */
                if (val < minVal)
                {
                    minVal = val;
                }
                if (filterVal < minFilterVal)
                {
                    minFilterVal = filterVal;
                }
                /* 统计最大值 */
                if (val > maxVal)
                {
                    maxVal = val;
                }
                if (filterVal > maxFilterVal)
                {
                    maxFilterVal = filterVal;
                }
            }

            /* 计算样本数 */
            int num = cursorEnd - cursorStart + 1;

            /* 计算平均值 */
            double avgVal = sumVal / num;
            double avgFilterVal = sumFilterVal / num;

            /* 计算波动范围 */
            double rangeVal = maxVal = minVal;
            double rangeFilterVal = maxFilterVal = minFilterVal;

            /* 样本数 */
            textBoxNum.Text = num.ToString();
            textBoxFilterNum.Text = num.ToString();
            /* 求和值 */
            textBoxSum.Text = sumVal.ToString();
            textBoxFilterSum.Text = sumFilterVal.ToString();
            /* 平均值 */
            textBoxAvg.Text = avgVal.ToString();
            textBoxFilterAvg.Text = avgFilterVal.ToString();
            /* 最小值 */
            textBoxMin.Text = minVal.ToString();
            textBoxFilterMin.Text = minFilterVal.ToString();
            /* 最大值 */
            textBoxMax.Text = maxVal.ToString();
            textBoxFilterMax.Text = maxFilterVal.ToString();
            /* 波动范围 */
            textBoxRange.Text = rangeVal.ToString();
            textBoxFilterRange.Text = rangeFilterVal.ToString();
        }

        private void checkBoxAutoCursor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoCursor.Checked)
            {
                buttonStatistical.Enabled = false;
                numericUpDownCursor1.Enabled = false;
                numericUpDownCursor2.Enabled = false;
                /* 游标自动选择数据范围 */
                CursorAutoSelect();
            }
            else
            {
                buttonStatistical.Enabled = true;
                numericUpDownCursor1.Enabled = true;
                numericUpDownCursor2.Enabled = true;
            }
        }
    }
}
