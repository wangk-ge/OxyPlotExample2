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
        private KalmanFilter m_kalmanFilter = new KalmanFilter(0.001f, 0.3f, 1.0f, 0);
        private DateTimeAxis m_xAxis; // X轴
        private LinearAxis m_yAxis; // Y轴

        private PlotModel m_plotModel;
        private Random rand = new Random(); // 用来生成随机数

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
            m_xAxis = new DateTimeAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalLength = 80,
                IsZoomEnabled = true,
                IsPanEnabled = true
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
                //Maximum = 100,
                //Minimum = -100
            };
            m_plotModel.Axes.Add(m_yAxis);

            var series = new LineSeries()
            {
                Color = OxyColors.Green,
                StrokeThickness = 1,
                MarkerSize = 1,
                MarkerStroke = OxyColors.DarkGreen,
                MarkerType = MarkerType.Diamond,
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
                Title = "AVG Filter Presure"
            };
            m_plotModel.Series.Add(series);

            series = new LineSeries()
            {
                Color = OxyColors.Blue,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerStroke = OxyColors.DarkBlue,
                MarkerType = MarkerType.Triangle,
                Title = "MID Filter Presure"
            };
            m_plotModel.Series.Add(series);

            plotView1.Model = m_plotModel;

#if false
            Task.Factory.StartNew(() =>
            {
                int[] filterBuf = new int[9];
                int filterCnt = 0;

                StreamReader mysr = new StreamReader("H:\\CSharpWork\\OxyPlotExample2\\OxyPlotExample2\\data.csv", System.Text.Encoding.Default);
                string strData = mysr.ReadToEnd();
                mysr.Close();
                mysr.Dispose();

                string[] strDataArray = strData.Split(new char[] {','});
                foreach (var strVal in strDataArray)
                {
                    if (String.Empty == strVal)
                    {
                        continue;
                    }
                    int val = Convert.ToInt32(strVal);
                    var delta = 1000 / 1000;
                    var date = DateTime.Now;
                    m_plotModel.Axes[0].Maximum = DateTimeAxis.ToDouble(date.AddSeconds(delta / 1000));

                    var lineSer1 = plotView1.Model.Series[0] as LineSeries;
                    lineSer1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), val));

                    filterBuf[filterCnt++] = val;
                    if (filterCnt >= filterBuf.Length)
                    {
                        Array.Sort(filterBuf);

                        int sumVal = 0;
                        for (int i = 1; i < filterBuf.Length - 1; i++)
                        {
                            sumVal += filterBuf[i];
                        }
                        int avgVal = sumVal / (filterBuf.Length - 2);
                        var lineSer2 = plotView1.Model.Series[1] as LineSeries;
                        lineSer2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), avgVal));

                        int midVal = filterBuf[filterBuf.Length / 2];
                        var lineSer3 = plotView1.Model.Series[2] as LineSeries;
                        lineSer3.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), midVal));

                        filterCnt = 0;
                    }

                    m_plotModel.InvalidatePlot(true);

                    Thread.Sleep(delta);
                }
            });
#else
            m_frameDecoder.WaveDataRespRecved += new FrameDecoder.WaveDataRecvHandler((byte channel, double value) => {
                Console.WriteLine($"WaveDataRespRecved: {channel} {value}");
                var delta = 1000 / 100;
                var date = DateTime.Now;
                m_plotModel.Axes[0].Maximum = DateTimeAxis.ToDouble(date.AddSeconds(delta / 1000));

                var lineSer1 = plotView1.Model.Series[0] as LineSeries;
                lineSer1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), value));

                var lineSer2 = plotView1.Model.Series[1] as LineSeries;
                lineSer2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), m_kalmanFilter.Input((float)value)));

                m_plotModel.InvalidatePlot(true);
            });

            Task.Factory.StartNew(() =>
            {
                StreamReader mysr = new StreamReader("H:\\CSharpWork\\OxyPlotExample2\\OxyPlotExample2\\2lPERS1.txt", System.Text.Encoding.Default);
                string strData = mysr.ReadToEnd();
                mysr.Close();
                mysr.Dispose();

                var delta = 1000 / 100;

                string[] strDataArray = strData.Split(new char[] { ' ' });
                foreach (var strVal in strDataArray)
                {
                    if (String.Empty == strVal)
                    {
                        continue;
                    }
                    byte val = Convert.ToByte(strVal, 16);

                    m_frameDecoder.FrameDecodeInput(val);

                    Thread.Sleep(delta);
                }
            });
#endif
            }
    }
}
