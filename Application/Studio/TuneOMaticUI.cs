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
using System.Globalization;

namespace DJetronicStudio
{
    public partial class TuneOMaticUI : UserControl, IUI
    {
        public event Action<object, ToolbarButton, bool> OnSetToolbarButtonState = null;
        public event Action<object, StatusLabel, string> OnSetStatusLabelText = null;

        private const int MPS_DATABASE_LAYOUT_PADDING = 10;

        private List<ToolbarButton> ToolbarButtons = new List<ToolbarButton>();
        private List<StatusLabel> StatusLabels = new List<StatusLabel>();
        private TuneOMatic Tuner;
        private bool Recording;
        private DateTime RecordingStartTime;
        private List<Tuple<double, double, UInt16>> RecordingBuffer = new List<Tuple<double, double, UInt16>>();
        private double LastPressure;
        private ToolbarButton TuneMPSButton;
        private ToolbarButton AddMPSButton;
        private ToolbarButton ImportMPSProfileButton;
        private int CurrentMPSDatabaseLayoutMaxColumns;

        public TuneOMaticUI
            (
            TuneOMatic Tuner
            )
        {
            this.Tuner = Tuner;

            InitializeComponent();

            TuneMPSButton = new ToolbarButton("Tune MPS", Properties.Resources.tuneomatic_32, TuneMPS, true);
            ToolbarButtons.Add(TuneMPSButton);

            AddMPSButton = new ToolbarButton("Add MPS to database", Properties.Resources.database_add_32, AddMPS, true);
            ToolbarButtons.Add(AddMPSButton);

            ImportMPSProfileButton = new ToolbarButton("Import MPS profile", Properties.Resources.import_mps_32, ImportMPSProfile, true);

            Tuner.OnReceivedPressure += Tuner_OnReceivedPressure;
            Tuner.OnReceivedPulseWidth += Tuner_OnReceivedPulseWidth;
            Recording = false;

            ShowInitialSettings();

            UpdateUI();
        }

        /// <summary>
        /// Called when user clicks on the previous button in the wizard
        /// Goes back a page
        /// </summary>
        /// <param name="sender"></param>
        private void Nav_OnPrevious(object sender)
        {
            if (Tabs.SelectedTab == TunePage2)
            {
                ShowPage(TunePage1);
            }
            else if (Tabs.SelectedTab == TunePage3)
            {
                ShowPage(TunePage2);
            }
            else if (Tabs.SelectedTab == TunePage4)
            {
                Tuner.RequestStopContinuousMeasurement();
                ShowPage(TunePage3);
            }
        }

        /// <summary>
        /// Called when user clicks on the next button in the wizard
        /// Validates and moves to the next page
        /// </summary>
        /// <param name="sender"></param>
        private void Nav_OnNext(object sender)
        {
            if (Tabs.SelectedTab == TunePage1)
            {
                ShowPage(TunePage2);
            }
            else if (Tabs.SelectedTab == TunePage2)
            {
                ShowPage(TunePage3);
            }
            else if (Tabs.SelectedTab == TunePage3)
            {
                ShowPage(TunePage4);
                Tuner.RequestStartContinuousMeasurement();
            }
            else if (Tabs.SelectedTab == TunePage4)
            {
                Tuner.RequestStopContinuousMeasurement();
                ShowPage(DbPage);
                if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, TuneMPSButton, true);
            }
        }

        /// <summary>
        /// Called when user cancels in a wizard
        /// Goes back to the database page
        /// </summary>
        /// <param name="sender"></param>
        private void Nav_OnCancel(object sender)
        {
            Tuner.RequestStopContinuousMeasurement();
            ShowPage(DbPage);
            if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, TuneMPSButton, true);
        }

        /// <summary>
        /// Called when user clicks on button to tune an MPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TuneMPS
            (
            object sender,
            EventArgs e
            )
        {
            ShowPage(TunePage1);
            if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, TuneMPSButton, false);
        }

        // from: https://stackoverflow.com/questions/76993/how-to-double-buffer-net-controls-on-a-form
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        /// <summary>
        /// Shows a page
        /// </summary>
        /// <param name="Page">Page to show</param>
        private void ShowPage
            (
            TabPage Page
            )
        {
            Tabs.SelectedTab = Page;

            if (Page == DbPage)
            {
                ShowDatabase();
            }
        }

        /// <summary>
        /// Shows the database
        /// </summary>
        private void ShowDatabase
            (
            )
        {
            if (Tabs.SelectedTab != DbPage)
            {
                return;
            }

            DbPage.Controls.Clear();

            int RowIndex = 0;
            int ColumnIndex = 0;

            MPSProfileUI ProfileUI = new MPSProfileUI();
            CurrentMPSDatabaseLayoutMaxColumns = DbPage.Width / (ProfileUI.Width + MPS_DATABASE_LAYOUT_PADDING);

            foreach (MPSProfile Profile in Tuner.Database.GetProfiles())
            {
                ProfileUI = new MPSProfileUI();
                ProfileUI.Profile = Profile;
                ProfileUI.Left = RowIndex * (ProfileUI.Width + MPS_DATABASE_LAYOUT_PADDING);
                ProfileUI.Top = ColumnIndex * (ProfileUI.Height + MPS_DATABASE_LAYOUT_PADDING);
                DbPage.Controls.Add(ProfileUI);

                if (++RowIndex == CurrentMPSDatabaseLayoutMaxColumns)
                {
                    RowIndex = 0;
                    ColumnIndex++;
                }
            }
        }

        /// <summary>
        /// Recalculates the layout of the database to fit the current
        /// size of the page
        /// </summary>
        private void RecalculateDatabaseLyout
            (
            )
        {
            if (DbPage.Controls.Count == 0)
            {
                return;
            }

            Control FirstControl = DbPage.Controls[0];

            int MaxColumns = DbPage.Width / (FirstControl.Width + MPS_DATABASE_LAYOUT_PADDING);
            if (MaxColumns == CurrentMPSDatabaseLayoutMaxColumns)
            {
                return;
            }

            CurrentMPSDatabaseLayoutMaxColumns = MaxColumns;

            int RowIndex = 0;
            int ColumnIndex = 0;

            foreach (Control Cntrl in DbPage.Controls)
            {
                Cntrl.Left = RowIndex * (Cntrl.Width + MPS_DATABASE_LAYOUT_PADDING);
                Cntrl.Top = ColumnIndex * (Cntrl.Height + MPS_DATABASE_LAYOUT_PADDING);

                if (++RowIndex == CurrentMPSDatabaseLayoutMaxColumns)
                {
                    RowIndex = 0;
                    ColumnIndex++;
                }
            }
        }

        /// <summary>
        /// Called when user clicks on button to add an MPS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddMPS
            (
            object sender,
            EventArgs e
            )
        {
            // fixme - to do
        }

        /// <summary>
        /// Called when user clicks on button to import an MPS profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ImportMPSProfile
            (
            object sender,
            EventArgs e
            )
        {
            if (ImportMPSProfileDialog.ShowDialog() == DialogResult.OK)
            {
                MPSProfile NewProfile = MPSProfile.ReadFromFile(ImportMPSProfileDialog.FileName);
                Tuner.Database.Add(NewProfile);
            }
        }

        /// <summary>
        /// Called when the tune-o-matic sends a pulse width
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="PulseWidth">Pulse width in us</param>
        private void Tuner_OnReceivedPulseWidth(object sender, ushort PulseWidth)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, ushort>(Tuner_OnReceivedPulseWidth), sender, PulseWidth);
                return;
            }

            PulseWidthValue.Text = string.Format("{0:N2}ms", PulseWidth / 1000.0);

            if (Recording)
            {
                RecordingBuffer.Add(new Tuple<double, double, UInt16>((DateTime.Now - RecordingStartTime).TotalMilliseconds, LastPressure, PulseWidth));
            }

            // if on tuning page then show pulse width on gauge
            if (Tabs.SelectedTab == TunePage4)
            {
                Gauge.Value = (float)(PulseWidth / 1000.0);
            }
        }

        /// <summary>
        /// Called when the tune-o-matic sends the atmospheric pressure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Pressure">Pressure in Pa</param>
        private void Tuner_OnReceivedPressure(object sender, double Pressure)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, double>(Tuner_OnReceivedPressure), sender, Pressure);
                return;
            }

            PressureValue.Text = string.Format("{0}", Pressure);

            if (Recording)
            {
                LastPressure = Pressure;
            }
        }

        /// <summary>
        /// Gets the toolbar buttons
        /// </summary>
        /// <returns>Enumeration of buttons</returns>
        public IEnumerable<ToolbarButton> GetToolbarButtons
            (
            )
        {
            foreach (ToolbarButton Button in ToolbarButtons)
            {
                yield return Button;
            }
        }

        /// <summary>
        /// Gets the status labels
        /// </summary>
        /// <returns>Enumeration of status labels</returns>
        public IEnumerable<StatusLabel> GetStatusLabels
            (
            )
        {
            foreach (StatusLabel Label in StatusLabels)
            {
                yield return Label;
            }
        }

        /// <summary>
        /// Shows the initial settings
        /// </summary>
        private void ShowInitialSettings
            (
            )
        {
            ShowPage(DbPage);
        }

        /// <summary>
        /// Updates the user interface with the current settings
        /// </summary>
        private void UpdateUI
            (
            )
        {
        }

        private void GetPressureBtn_Click(object sender, EventArgs e)
        {
            Tuner.RequestPressure();
        }

        private void GetPulseWidthBtn_Click(object sender, EventArgs e)
        {
            Tuner.RequestPulseWidth();
        }

        private void StartContBtn_Click(object sender, EventArgs e)
        {
            Tuner.RequestStartContinuousMeasurement();
            RecordingBuffer.Clear();
            LastPressure = 0;
            RecordingStartTime = DateTime.Now;
            Recording = true;
        }

        private void StopContBtn_Click(object sender, EventArgs e)
        {
            Tuner.RequestStopContinuousMeasurement();
            Recording = false;

            if (ExportCSVDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter CSVFile = new StreamWriter(ExportCSVDialog.FileName, false, Encoding.ASCII);

                try
                {
                    CSVFile.WriteLine("\"Time (ms)\",\"Pressure (Pa)\",\"Pulse Width (ms)\"");

                    foreach (Tuple<double, double, UInt16> DataPoint in RecordingBuffer)
                    {
                        CSVFile.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\"",
                            DataPoint.Item1.ToString(),
                            DataPoint.Item2.ToString(CultureInfo.CreateSpecificCulture("en-US")),
                            (DataPoint.Item3 / 1000.0).ToString(CultureInfo.CreateSpecificCulture("en-US"))
                            ));
                    }
                }
                finally
                {
                    CSVFile.Close();
                }
            }
        }

        /// <summary>
        /// Called when the database page is resized
        /// Updates the grid of profiles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DbPage_Resize(object sender, EventArgs e)
        {
            RecalculateDatabaseLyout();
        }
    }
}
