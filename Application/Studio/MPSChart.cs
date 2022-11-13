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
    public partial class MPSChart : UserControl
    {
        /// <summary>
        /// Sea level pressure in pascals
        /// </summary>
        private const int SEA_LEVEL_PA = 101325;

        private ObservableCollection<ISeries> Series { get; set; }
        private Random RandomGenerator = new Random();

        private MPSDatabase _Database = null;
        public MPSDatabase Database
        {
            get { return _Database; }
            set
            {
                _Database = value;
                BuildTree();
            }
        }

        public MPSChart()
        {
            InitializeComponent();

            Series = new ObservableCollection<ISeries> { };

            Chart.XAxes = new LiveChartsCore.Axis<SkiaSharpDrawingContext, LabelGeometry, LineGeometry>[]
            {
                new LiveChartsCore.SkiaSharpView.Axis
                {
                    TextSize = 12,
                    Name = "Vacuum (inHg)",
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
                    MinLimit = 16,
                    MaxLimit = 18.5,
                    NameTextSize = 16,
                    Name = "Pulse Width (ms)",
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
                Text = "Pulse Width vs Vacuum for Select MPSs at Sea Level Pressure",
                TextSize = 18,
                Padding = new LiveChartsCore.Drawing.Padding(15),
                Paint = new SolidColorPaint(SKColors.Black)
            };

            //((LiveChartsCore.SkiaSharpView.WinForms.DefaultLegend)Chart.Legend).Visible = true;
        }

        /// <summary>
        /// Builds a tree of the MPS database
        /// </summary>
        private void BuildTree
            (
            )
        {
            DatabaseTree.Nodes.Clear();

            ImageList Images = new ImageList();

            // set up image list
            Images.Images.Add(Properties.Resources.mps_128);
            DatabaseTree.ImageList = Images;

            foreach (MPSProfile Profile in Database.GetProfiles())
            {
                TreeNode MPSNode = new TreeNode(Profile.Name, 0, 0);
                MPSNode.Checked = false;
                MPSNode.Tag = Profile;
                DatabaseTree.Nodes.Add(MPSNode);
            }
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
        /// Called after user selects or deselects an MPS to be displayed on the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            MPSProfile Profile = e.Node.Tag as MPSProfile;
            if (e.Node.Checked)
            {
                AddSeries(Profile, e.Node);
            }
            else
            {
                RemoveSeries(Profile, e.Node);
            }
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

            double[] AdjPulseWidths = Profile.GetAdjustedPulseWidths(SEA_LEVEL_PA);

            for (int p = 0; p <= MPSProfile.MAX_VACUUM; p++)
            {
                ObservablePoint NewData = new ObservablePoint(p, AdjPulseWidths[p] / 1000.0);
                ((ObservableCollection<ObservablePoint>)NewSeries.Values).Add(NewData);
            }

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
