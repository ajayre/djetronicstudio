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
        public delegate void OnRequestAddCustomDataHandler(object sender, string VectorName);
        public event OnRequestAddCustomDataHandler OnRequestAddCustomData = null;

        private ObservableCollection<ISeries> Series { get; set; }
        private Random RandomGenerator = new Random();
        private List<SimData> AvailableData = new List<SimData>();
        private SimChartSettings ChartSettings = new SimChartSettings();

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
                    MinLimit = -25,
                    MaxLimit = 17,
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
        /// <param name="Name">Display name</param>
        /// <param name="VectorName">Simulation vector name</param>
        /// <param name="DataSource">The source of the data</param>
        /// <param name="Data">Set of time-value data points</param>
        public void AddData
            (
            string Name,
            string VectorName,
            SimData.DataSources DataSource,
            List<NGSpice.SimDataPoint> Data
            )
        {
            if (Data == null) return;

            SimData SData = new SimData(Name, VectorName, DataSource, Data);
            AvailableData.Add(SData);
        }

        /// <summary>
        /// Finds simulation data by vector name
        /// </summary>
        /// <param name="VectorName">Vector name to search for</param>
        /// <returns>Simulation data or null if not found</returns>
        private SimData GetDataFromVectorName
            (
            string VectorName
            )
        {
            foreach (SimData Data in AvailableData)
            {
                if (Data.VectorName == VectorName)
                {
                    return Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds simulation data by data source
        /// </summary>
        /// <param name="DataSource">Data source to search for</param>
        /// <returns>Simulation data of null if not found</returns>
        private SimData GetDataFromDataSource
            (
            SimData.DataSources DataSource
            )
        {
            foreach (SimData Data in AvailableData)
            {
                if (Data.DataSource == DataSource)
                {
                    return Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Shows data on the chart
        /// </summary>
        /// <param name="VectorName">Name of the simulation vector to show</param>
        /// <param name="LineColor">Color of line</param>
        private void ShowData
            (
            SimData Data,
            Color LineColor
            )
        {
            LineSeries<ObservablePoint> NewSeries = new LineSeries<ObservablePoint>();
            NewSeries.Name = Data.Name;
            NewSeries.LineSmoothness = 0.0;
            NewSeries.Fill = null;
            NewSeries.GeometrySize = 0;
            NewSeries.GeometryStroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 1.0F };
            NewSeries.Stroke = new SolidColorPaint(new SKColor(LineColor.R, LineColor.G, LineColor.B, LineColor.A)) { StrokeThickness = 2.0F };
            NewSeries.Values = new ObservableCollection<ObservablePoint>();
            NewSeries.Tag = Data;
            Series.Add(NewSeries);

            for (int p = 0; p < Data.Points.Count; p++)
            {
                ObservablePoint NewData = new ObservablePoint(Data.Points[p].Time * 1000, Data.Points[p].Value);
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

        public class SimData
        {
            public enum DataSources
            {
                Custom,
                InjectorGroupI,
                InjectorGroupII,
                InjectorGroupIII,
                InjectorGroupIV,
                MPSPin7,
                MPSPin8,
                MPSPin10,
                MPSPin15,
                AirTemperature,
                CoolantTemperature,
                TPSFullThrottle_DiagII_IV,
                TPSIdle,
                TPSAcceleration1,
                TPSAcceleration2,
                PulseGeneratorGroupI,
                PulseGeneratorGroupII,
                PulseGeneratorGroupIII,
                PulseGeneratorGroupIV,
                Start,
                FuelPumpRelay,
                DiagI_III
            }

            public string Name;
            public string VectorName;
            public List<NGSpice.SimDataPoint> Points;
            public DataSources DataSource;

            public SimData
                (
                string Name,
                string VectorName,
                DataSources DataSource,
                List<NGSpice.SimDataPoint> Points
                )
            {
                this.Name = Name;
                this.VectorName = VectorName;
                this.DataSource = DataSource;
                this.Points = Points;
            }
        }

        /// <summary>
        /// Called when user clicks on the edit button
        /// Shows the settings form and applies new settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            SimChartSettingsForm SettingsForm = new SimChartSettingsForm();
            SettingsForm.Settings = ChartSettings;
            if (SettingsForm.ShowDialog() == DialogResult.OK)
            {
                Clear();

                if (ChartSettings.ShowInjectorGroupI)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.InjectorGroupI);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.InjectorGroupIColor);
                    }
                }
                if (ChartSettings.ShowInjectorGroupII)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.InjectorGroupII);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.InjectorGroupIIColor);
                    }
                }
                if (ChartSettings.ShowInjectorGroupIII)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.InjectorGroupIII);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.InjectorGroupIIIColor);
                    }
                }
                if (ChartSettings.ShowInjectorGroupIV)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.InjectorGroupIV);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.InjectorGroupIVColor);
                    }
                }

                if (ChartSettings.ShowMPSPin7)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.MPSPin7);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.MPSPin7Color);
                    }
                }
                if (ChartSettings.ShowMPSPin8)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.MPSPin8);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.MPSPin8Color);
                    }
                }
                if (ChartSettings.ShowMPSPin10)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.MPSPin10);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.MPSPin10Color);
                    }
                }
                if (ChartSettings.ShowMPSPin15)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.MPSPin15);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.MPSPin15Color);
                    }
                }

                if (ChartSettings.ShowCustom)
                {
                    SimData Data = GetDataFromVectorName(ChartSettings.CustomVectorName);
                    if (Data == null)
                    {
                        if (OnRequestAddCustomData != null) OnRequestAddCustomData(this, ChartSettings.CustomVectorName);
                        Data = GetDataFromVectorName(ChartSettings.CustomVectorName);
                    }

                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.CustomColor);
                    }
                }

                if (ChartSettings.ShowPulseGeneratorGroupI)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.PulseGeneratorGroupI);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.PulseGeneratorGroupIColor);
                    }
                }
                if (ChartSettings.ShowPulseGeneratorGroupII)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.PulseGeneratorGroupII);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.PulseGeneratorGroupIIColor);
                    }
                }
                if (ChartSettings.ShowPulseGeneratorGroupIII)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.PulseGeneratorGroupIII);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.PulseGeneratorGroupIIIColor);
                    }
                }
                if (ChartSettings.ShowPulseGeneratorGroupIV)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.PulseGeneratorGroupIV);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.PulseGeneratorGroupIVColor);
                    }
                }

                if (ChartSettings.ShowTPSIdle)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.TPSIdle);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.TPSIdleColor);
                    }
                }
                if (ChartSettings.ShowTPSFullThrottle)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.TPSFullThrottle_DiagII_IV);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.TPSFullThrottleColor);
                    }
                }
                if (ChartSettings.ShowTPSAcceleration1)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.TPSAcceleration1);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.TPSAcceleration1Color);
                    }
                }
                if (ChartSettings.ShowTPSAcceleration2)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.TPSAcceleration2);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.TPSAcceleration2Color);
                    }
                }

                if (ChartSettings.ShowAirTemperature)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.AirTemperature);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.AirTemperatureColor);
                    }
                }
                if (ChartSettings.ShowCoolantTemperature)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.CoolantTemperature);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.CoolantTemperatureColor);
                    }
                }

                if (ChartSettings.ShowDiagI_III)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.DiagI_III);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.DiagI_IIIColor);
                    }
                }

                if (ChartSettings.ShowStart)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.Start);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.StartColor);
                    }
                }
                if (ChartSettings.ShowFuelPumpRelay)
                {
                    SimData Data = GetDataFromDataSource(SimData.DataSources.FuelPumpRelay);
                    if (Data != null)
                    {
                        ShowData(Data, ChartSettings.FuelPumpRelayColor);
                    }
                }
            }
        }
    }
}
