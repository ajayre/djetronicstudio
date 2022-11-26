using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
    public partial class SimChart : UserControl
    {
        private ObservableCollection<ISeries> Series { get; set; }
        private Random RandomGenerator = new Random();

        public SimChart()
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
                    MinLimit = -16,
                    MaxLimit = 12,
                    NameTextSize = 16,
                    Name = "Voltage (V)",
                    Position = AxisPosition.Start,
                    NamePaint = new SolidColorPaint(new SKColor(0, 0, 0)),
                    LabelsPaint = new SolidColorPaint(new SKColor(0, 0, 0))
                }
            };

            Chart.Series = Series;
            Chart.AnimationsSpeed = TimeSpan.FromMilliseconds(300);
            Chart.EasingFunction = null;
            Chart.Title = new LabelVisual
            {
                Text = "Simulation Data",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black)
            };
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
        /// Called when user clicks on the button to export the chart as an image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportImageBtn_Click(object sender, EventArgs e)
        {
            ExportImage();
        }

        /// <summary>
        /// Adds data that the user can choose to display on the chart
        /// </summary>
        /// <param name="SeriesName">Display name</param>
        /// <param name="VectorName">Simulation vector name</param>
        /// <param name="Data">Set of time-value data points</param>
        public void AddData
            (
            string SeriesName,
            string VectorName,
            List<NGSpice.SimDataPoint> Data
            )
        {
            Color LineColor = Color.Red;

            LineSeries<ObservablePoint> NewSeries = new LineSeries<ObservablePoint>();
            NewSeries.Name = SeriesName;
            NewSeries.LineSmoothness = 0.0;
            NewSeries.Fill = null;
            NewSeries.GeometrySize = 0;
            NewSeries.GeometryStroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 1.0F };
            NewSeries.Stroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 2.0F };
            NewSeries.Values = new ObservableCollection<ObservablePoint>();
            NewSeries.Tag = VectorName;
            Series.Add(NewSeries);

            for (int p = 0; p < Data.Count; p++)
            {
                ObservablePoint NewData = new ObservablePoint(Data[p].Time * 1000, Data[p].Value);
                ((ObservableCollection<ObservablePoint>)NewSeries.Values).Add(NewData);
            }

            Chart.CoreChart.Update(new LiveChartsCore.Kernel.ChartUpdateParams { IsAutomaticUpdate = false, Throttling = false });
        }

        /// <summary>
        /// Clears the data from the chart
        /// </summary>
        public void Clear
            (
            )
        {
            foreach (LineSeries<ObservablePoint> Ser in Series)
            {
                ((ObservableCollection<ObservablePoint>)Ser.Values).Clear();
            }

            Series.Clear();
        }

        /// <summary>
        /// Adds a series to the chart
        /// </summary>
        /// <param name="Profile">Profile to show on chart</param>
        /// <param name="TNode">Tree node associated with profile</param>
        private void AddSeries
            (
            MPSProfile Profile,
            TreeNode TNode
            )
        {
            Color LineColor = Color.FromArgb(RandomGenerator.Next(256), RandomGenerator.Next(256), RandomGenerator.Next(256));

            LineSeries<ObservablePoint> NewSeries = new LineSeries<ObservablePoint>();
            NewSeries.Name = Profile.Name;
            NewSeries.LineSmoothness = 0.0;
            NewSeries.Fill = null;
            NewSeries.GeometrySize = 8.0;
            NewSeries.GeometryStroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 1.0F };
            NewSeries.Stroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 2.0F };
            NewSeries.Values = new ObservableCollection<ObservablePoint>();
            NewSeries.Tag = Profile;
            Series.Add(NewSeries);

            //for (int p = 0; p <= MPSProfile.MAX_VACUUM; p++)
            //{
            //    ObservablePoint NewData = new ObservablePoint(p, AdjPulseWidths[p] / 1000.0);
            //    ((ObservableCollection<ObservablePoint>)NewSeries.Values).Add(NewData);
            //}

            Chart.CoreChart.Update(new LiveChartsCore.Kernel.ChartUpdateParams { IsAutomaticUpdate = false, Throttling = false });

            TNode.ForeColor = LineColor;
        }

        /// <summary>
        /// Removes a series from the chart
        /// </summary>
        /// <param name="Profile">Profile to remove from chart</param>
        /// <param name="TNode">Tree node associated with profile</param>
        private void RemoveSeries
            (
            MPSProfile Profile,
            TreeNode TNode
            )
        {
            foreach (LineSeries<ObservablePoint> Ser in Series)
            {
                if (Ser.Tag == Profile)
                {
                    ((ObservableCollection<ObservablePoint>)Ser.Values).Clear();
                    Series.Remove(Ser);
                    TNode.ForeColor = Color.Black;
                    return;
                }
            }
        }
    }
}
