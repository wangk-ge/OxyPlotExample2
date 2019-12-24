using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OxyPlotExample2
{
    public partial class Form1 : Form
    {
        private FrameDecoder m_frameDecoder = new FrameDecoder();
        private FlowSensor m_flowSensor = new FlowSensor();
        private KalmanFilter m_kalmanFilter = new KalmanFilter(0.01f/*Q*/, 0.1f/*R*/, 1.0f/*P*/, 0);
        private KalmanFilter m_kalmanFilter2 = new KalmanFilter(0.01f/*Q*/, 3.0f/*R*/, 10.0f/*P*/, 0);
        private LinearAxis m_xAxis; // X轴
        private LinearAxis m_yAxis; // Y轴
        private int m_X = 0;

        private PlotModel m_plotModel;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                IntervalLength = 60,
                IsZoomEnabled = true,
                IsPanEnabled = true,
                Position = AxisPosition.Bottom
            };
            m_plotModel.Axes.Add(m_xAxis);

            //Y轴
            m_yAxis = new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalLength = 60,
                Angle = 0,
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
                Title = "Filter1"
            };
            m_plotModel.Series.Add(series);

            series = new LineSeries()
            {
                Color = OxyColors.Blue,
                StrokeThickness = 1,
                MarkerSize = 1,
                MarkerStroke = OxyColors.DarkBlue,
                MarkerType = MarkerType.Circle,
                Title = "Filter2"
            };
            m_plotModel.Series.Add(series);

            plotView1.Model = m_plotModel;

#if false
            /* 通过文件获取数据 */
            m_frameDecoder.WaveDataRespRecved += new FrameDecoder.WaveDataRecvHandler((byte channel, double value) => {
                Console.WriteLine($"WaveDataRespRecved: {channel} {value}");
                m_plotModel.Axes[0].Maximum = m_X + 1;

                var lineSer1 = plotView1.Model.Series[0] as LineSeries;
                lineSer1.Points.Add(new DataPoint(m_X, value));

                var lineSer2 = plotView1.Model.Series[1] as LineSeries;
                lineSer2.Points.Add(new DataPoint(m_X, m_kalmanFilter.Input((float)value)));

                var lineSer3 = plotView1.Model.Series[2] as LineSeries;
                lineSer3.Points.Add(new DataPoint(m_X, m_kalmanFilter2.Input((float)value)));

                m_X++;

                m_plotModel.InvalidatePlot(true);
            });

            Task.Factory.StartNew(() =>
            {
                StreamReader mysr = new StreamReader("H:\\CSharpWork\\OxyPlotExample2\\OxyPlotExample2\\2LPERS.txt", System.Text.Encoding.Default);
                string strData = mysr.ReadToEnd();
                mysr.Close();
                mysr.Dispose();

                string[] strDataArray = strData.Split(new char[] { ' ' });
                foreach (var strVal in strDataArray)
                {
                    if (String.Empty == strVal)
                    {
                        continue;
                    }
                    byte val = Convert.ToByte(strVal, 16);

                    m_frameDecoder.FrameDecodeInput(val);
                }
            });
#else
            /* 通过传感器获取数据 */
            m_flowSensor.m_frameDecoder.WaveDataRespRecved += new FrameDecoder.WaveDataRecvHandler((byte channel, double value) => {
                Console.WriteLine($"WaveDataRespRecved: {channel} {value}");

                m_plotModel.Axes[0].Maximum = m_X + 1;

                var lineSer1 = plotView1.Model.Series[0] as LineSeries;
                lineSer1.Points.Add(new DataPoint(m_X, value / 1200));
                if (lineSer1.Points.Count > 2000)
                {
                    lineSer1.Points.RemoveAt(0);
                }

                var lineSer2 = plotView1.Model.Series[1] as LineSeries;
                lineSer2.Points.Add(new DataPoint(m_X, m_kalmanFilter.Input((float)value) / 1200));
                if (lineSer2.Points.Count > 2000)
                {
                    lineSer2.Points.RemoveAt(0);
                }

                var lineSer3 = plotView1.Model.Series[2] as LineSeries;
                lineSer3.Points.Add(new DataPoint(m_X, m_kalmanFilter2.Input((float)value) / 1200));
                if (lineSer3.Points.Count > 2000)
                {
                    lineSer3.Points.RemoveAt(0);
                }

                m_X++;

                m_plotModel.InvalidatePlot(true);
            });

            m_flowSensor.Open("COM4");
#endif
        }

        private async void SendCmd(string cmd)
        {
            this.textBoxInfo.AppendText($"Sned: {cmd} \r\n");
            string cmdResp = await m_flowSensor.ExcuteCmdAsync(cmd, 2000);
            this.textBoxInfo.AppendText($"Revc: {cmdResp} \r\n");
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            SendCmd("[ADC_START]");
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            SendCmd("[ADC_STOP]");
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            SendCmd("[ADC_CAL]");
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendCmd(this.textBoxCmd.Text);
        }
    }
}
