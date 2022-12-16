using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Defaults;
using LiveChartsCore.Motion;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.SKCharts;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using LiveChartsCore.Drawing;
using SkiaSharp;
using SkiaSharp.Internals;

namespace DJetronicStudio
{
    public partial class WiringDiagramChartForm : Form
    {
        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        private ObservableCollection<ISeries> Series { get; set; }

        public WiringDiagramChartForm()
        {
            InitializeComponent();

            Series = new ObservableCollection<ISeries> { };

            Chart.XAxes = new LiveChartsCore.Axis<SkiaSharpDrawingContext, LabelGeometry, LineGeometry>[]
            {
                new LiveChartsCore.SkiaSharpView.Axis
                {
                    TextSize = 12,
                    Name = "Time (ms)",
                    NameTextSize = 16,
                    MinLimit = 0,
                    MaxLimit = 15
                }
            };

            Chart.YAxes = new Axis[]
            {
                new Axis
                {
                    TextSize = 12,
                    NameTextSize = 16,
                    Name = "Voltage (V)",
                    Position = AxisPosition.Start,
                    NamePaint = new SolidColorPaint(new SKColor(0, 0, 0)),
                    LabelsPaint = new SolidColorPaint(new SKColor(0, 0, 0))
                }
            };

            Chart.Series = Series;
            Chart.AnimationsSpeed = TimeSpan.FromMilliseconds(300);
            Chart.Title = new LabelVisual
            {
                Text = "Simulation Data",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black)
            };
            Chart.ZoomMode = ZoomAndPanMode.Both;
            Chart.LegendPosition = LegendPosition.Hidden;
            Chart.TooltipPosition = TooltipPosition.Hidden;
        }

        /// <summary>
        /// Sets the time range for the X axis
        /// </summary>
        /// <param name="StartTimeMs">Start time in ms</param>
        /// <param name="EndTimeMs">End time in ms</param>
        public void SetTimeRange
            (
            double StartTimeMs,
            double EndTimeMs
            )
        {
            Chart.XAxes.First().MinLimit = StartTimeMs;
            Chart.XAxes.First().MaxLimit = EndTimeMs;
        }

        /// <summary>
        /// Adds data that the user can choose to display on the chart
        /// </summary>
        /// <param name="Name">Display name</param>
        /// <param name="Data">Set of time-value data points</param>
        public void AddData
            (
            string Name,
            List<NGSpice.SimDataPoint> Data
            )
        {
            if (Data == null) return;

            Color LineColor = Color.Red;

            LineSeries<ObservablePoint> NewSeries = new LineSeries<ObservablePoint>();
            NewSeries.Name = Name;
            NewSeries.LineSmoothness = 0.0;
            NewSeries.Fill = null;
            NewSeries.GeometrySize = 0;
            NewSeries.GeometryStroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 1.0F };
            NewSeries.Stroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 2.0F };
            NewSeries.Values = new ObservableCollection<ObservablePoint>();
            NewSeries.Tag = Data;
            Series.Add(NewSeries);

            for (int p = 0; p < Data.Count; p++)
            {
                ObservablePoint NewData = new ObservablePoint(Data[p].Time * 1000, Data[p].Value);
                ((ObservableCollection<ObservablePoint>)NewSeries.Values).Add(NewData);
            }

            Chart.CoreChart.Update(new LiveChartsCore.Kernel.ChartUpdateParams { IsAutomaticUpdate = false, Throttling = false });
        }

        /// <summary>
        /// Called when user clicks on the button to export the chart as an image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportImageBtn_Click(object sender, EventArgs e)
        {
            ExportImage();
        }

        /// <summary>
        /// Exports the chart as an image
        /// </summary>
        private void ExportImage
            (
            )
        {
            if (ExportImageDialog.ShowDialog() == DialogResult.OK)
            {
                var Ch = new SKCartesianChart(Chart);
                //Ch.Width = 1000;
                //Ch.Height = 300;
                var Img = Ch.GetImage();

                SKEncodedImageFormat Format = SKEncodedImageFormat.Png;
                string Ext = Path.GetExtension(ExportImageDialog.FileName);
                if (Ext == ".gif") Format = SKEncodedImageFormat.Gif;
                else if (Ext == ".bmp") Format = SKEncodedImageFormat.Bmp;
                else if (Ext == ".jpg") Format = SKEncodedImageFormat.Jpeg;
                else if (Ext == ".jpeg") Format = SKEncodedImageFormat.Jpeg;

                Ch.SaveImage(ExportImageDialog.FileName, Format);
            }
        }

        /// <summary>
        /// Called when form is shown
        /// Configures chart and shows data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WiringDiagramChartForm_Shown(object sender, EventArgs e)
        {
            ShowData();
        }

        /// <summary>
        /// Shows the data on the chart
        /// </summary>
        private void ShowData
            (
            )
        {

        }
    }
}
