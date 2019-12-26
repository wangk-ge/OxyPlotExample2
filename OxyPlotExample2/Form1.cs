using OxyPlot;
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
        private int m_X = 0;
        private Timer m_refreshTimer = new Timer();

        private PlotModel m_plotModel;

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
                Title = "Presure",
                LegendTitle = "Legend",
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
                //IntervalLength = 60,
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
                //IntervalLength = 60,
                //Angle = 0,
                IsZoomEnabled = true,
                IsPanEnabled = true,
                Position = AxisPosition.Left,
                //Minimum = -1000,
                //Maximum = 1000
            };
            m_plotModel.Axes.Add(m_yAxis);

            var series = new LineSeries()
            {
                Color = OxyColors.Green,
                StrokeThickness = 1,
                MarkerSize = 1,
                MarkerStroke = OxyColors.DarkGreen,
                MarkerType = MarkerType.Circle,
                Title = "Presure"
            };
            m_plotModel.Series.Add(series);

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

            plotView1.Model = m_plotModel;

            /* 通过传感器获取数据 */
            m_flowSensor.m_frameDecoder.WaveDataRespRecved += new FrameDecoder.WaveDataRecvHandler((byte channel, double value) => {
                //Console.WriteLine($"WaveDataRespRecved: {channel} {value}");

                m_dataQueue.Enqueue(value);
            });

            m_refreshTimer.Interval = 1000 / 24;
            m_refreshTimer.Tick += new EventHandler((timer, arg) => {

                while (m_dataQueue.Count > 0)
                {
                    bool bRet = m_dataQueue.TryDequeue(out double val);
                    if (!bRet)
                    {
                        break;
                    }
                    AddPoint(val);
                }
            });

            m_kalmanFilter.R = (float)decimal.ToDouble(numericUpDownR.Value);
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

            m_plotModel.InvalidatePlot(true);
        }

        private void AddPoint(double val)
        {
            var lineSer1 = plotView1.Model.Series[0] as LineSeries;
            lineSer1.Points.Add(new DataPoint(m_X, val));

            var lineSer2 = plotView1.Model.Series[1] as LineSeries;
            lineSer2.Points.Add(new DataPoint(m_X, m_kalmanFilter.Input((float)val)));

            m_X++;

            m_plotModel.InvalidatePlot(true);

            var xAxis = m_plotModel.Axes[0];
            if (m_X > xAxis.Maximum)
            {
                double panStep = xAxis.Transform(-1 + xAxis.Offset);
                xAxis.Pan(panStep);
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
                numericUpDownR.Enabled = true;
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
                    numericUpDownR.Enabled = false;
                    toolStripButtonClear.Enabled = false;
                    toolStripButtonLoad.Enabled = false;
                    toolStripButtonSave.Enabled = false;
                    ClearAll();
                    /* 启动刷新定时器 */
                    m_refreshTimer.Start();
                }
                else // if ("停止" == toolStripButtonStart.Text)
                {
                    toolStripButtonStart.Text = "开始";
                    toolStripButtonZero.Enabled = false;
                    buttonFilterApply.Enabled = true;
                    numericUpDownR.Enabled = true;
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
    }
}
