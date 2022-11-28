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

namespace DJetronicStudio
{
    public partial class ECUTesterUI : UserControl, IUI
    {
        private const int DEFAULT_DYNAMIC_TIME_MS = 10000;
        private const int DEFAULT_DYNAMIC_RESOLUTION_MS = 50;

        private DataBuffer Buffer = new DataBuffer();
        private bool FirstStatus = true;
        private bool Recording = false;
        private DateTime RecordingStartTime;
        private ECUTester Tester = null;
        private ToolbarButton StartRecordingButton;
        private ToolbarButton StopRecordingButton;
        private ToolbarButton ExportCSVButton;
        private StatusLabel EngineNameIndicator;
        private StatusLabel BufferStatus;
        private List<ToolbarButton> ToolbarButtons = new List<ToolbarButton>();
        private List<StatusLabel> StatusLabels = new List<StatusLabel>();

        public event Action<object, ToolbarButton, bool> OnSetToolbarButtonState = null;
        public event Action<object, StatusLabel, string> OnSetStatusLabelText = null;
        public event Action<object, double> OnPercentageCompleted = null;

        public ECUTesterUI
            (
            ECUTester Tester
            )
        {
            this.Tester = Tester;

            InitializeComponent();

            Tester.OnShowMessage += Tester_OnShowMessage;
            Tester.OnReceivedStatus += Tester_OnReceivedStatus;
            Tester.OnReceivedEngineName += Tester_OnReceivedEngineName;
            Tester.OnDynamicTestStarted += Tester_OnDynamicTestStarted;
            Tester.OnDynamicTestStopped += Tester_OnDynamicTestStopped;

            Buffer.Clear();
            TesterInfoBox.Text = "";
            OutputBox.Text = "";

            StartRecordingButton = new ToolbarButton("Start recording data", Properties.Resources.record, StartRecording, true);
            ToolbarButtons.Add(StartRecordingButton);

            StopRecordingButton = new ToolbarButton("Stop recording data", Properties.Resources.stop, StopRecording, false);
            ToolbarButtons.Add(StopRecordingButton);

            ExportCSVButton = new ToolbarButton("Export captured data as CSV", Properties.Resources.csv_32, ExportCSV, false);
            ToolbarButtons.Add(ExportCSVButton);

            EngineNameIndicator = new StatusLabel("Unknown", Properties.Resources.car_24, 127);
            StatusLabels.Add(EngineNameIndicator);

            BufferStatus = new StatusLabel("Empty", Properties.Resources.database_24, 95);
            StatusLabels.Add(BufferStatus);

#if DEBUG
            Gallery.ImageFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\..\..\..\Documentation";
#else
            Tabs.TabPages.Remove(DebugOutputPage);
            Tabs.TabPages.Remove(AutomatedTestPage);
            Gallery.ImageFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\Documentation";
#endif
            Gallery.OnShowImage += Gallery_OnShowImage;

            DynamicProgressBar.Style = ProgressBarStyle.Continuous;

            Buffer.OnDataAdded += Buffer_OnDataAdded;
            Buffer.OnDataCleared += Buffer_OnDataCleared;

            Buffer.Clear();

            Chart.Buffer = Buffer;

            Tester.UsePreset_EngineOff();
            PresetSelector.SelectedIndex = PresetSelector.Items.IndexOf("Engine Off");

            ShowInitialSettings();

            UpdateUI();
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
        /// Called when the user interface has been constructed
        /// </summary>
        public void UIReady
            (
            )
        {
        }

        /// <summary>
        /// Called when user clicks on button to start recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StartRecording
            (
            object sender,
            EventArgs e
            )
        {
            if (!Recording)
            {
                Recording = true;
                Buffer.Clear();
                RecordingStartTime = DateTime.Now;
                UpdateUI();
                if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, StartRecordingButton, false);
                if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, StopRecordingButton, true);
            }
        }

        /// <summary>
        /// Called when user clicks on button to stop recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StopRecording
            (
            object sender,
            EventArgs e
            )
        {
            if (Recording)
            {
                Recording = false;
                UpdateUI();
                if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, StartRecordingButton, true);
                if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, StopRecordingButton, false);
            }
        }

        /// <summary>
        /// Called when user clicks on button to export captured data to CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExportCSV
            (
            object sender,
            EventArgs e
            )
        {
            if (ExportCSVDialog.ShowDialog() == DialogResult.OK)
            {
                Buffer.ExportCSV(ExportCSVDialog.FileName, Application.ProductName, Application.ProductVersion);
            }
        }

        /// <summary>
        /// Called when buffer is cleared
        /// </summary>
        /// <param name="sender"></param>
        private void Buffer_OnDataCleared(object sender)
        {
            if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, ExportCSVButton, false);
            if (OnSetStatusLabelText != null) OnSetStatusLabelText(this, BufferStatus, "Empty");
        }

        /// <summary>
        /// Called when data is added to the buffer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NumPoints"></param>
        private void Buffer_OnDataAdded(object sender, int NumPoints, DataPoint NewPoint)
        {
            if (OnSetToolbarButtonState != null) OnSetToolbarButtonState(this, ExportCSVButton, true);
            if (OnSetStatusLabelText != null) OnSetStatusLabelText(this, BufferStatus, NumPoints.ToString() + " entries");
        }

        /// <summary>
        /// The dynamic test has stopped
        /// </summary>
        /// <param name="sender"></param>
        private void Tester_OnDynamicTestStopped(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object>(Tester_OnDynamicTestStopped), sender);
                return;
            }

            DynamicProgressBar.Style = ProgressBarStyle.Continuous;
            DynamicStartStopBtn.Text = "Start";
        }

        /// <summary>
        /// A dynamic test has started
        /// </summary>
        /// <param name="sender"></param>
        private void Tester_OnDynamicTestStarted(object sender)
        {
            DynamicProgressBar.Style = ProgressBarStyle.Marquee;
            DynamicStartStopBtn.Text = "Stop";
        }

        /// <summary>
        /// Called when the engine name has been sent from the tester
        /// Displays the name in the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="EngineName"></param>
        private void Tester_OnReceivedEngineName(object sender, string EngineName)
        {
            if (OnSetStatusLabelText != null) OnSetStatusLabelText(this, EngineNameIndicator, EngineName);
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
        /// Shows the initial settings
        /// </summary>
        private void ShowInitialSettings
            (
            )
        {
            DynamicTimePeriod.Text = DEFAULT_DYNAMIC_TIME_MS.ToString();
            DynamicResolutionInput.Text = DEFAULT_DYNAMIC_RESOLUTION_MS.ToString();
        }

        /// <summary>
        /// Called when user wishes to view an image from the documentation
        /// </summary>
        /// <param name="sender">Gallery</param>
        /// <param name="FileName">Path and name of image</param>
        private void Gallery_OnShowImage(object sender, string FileName, Image image)
        {
            ImageViewerForm Viewer = new ImageViewerForm();
            Viewer.DisplayImage = image;
            Viewer.ImageTitle = Path.GetFileNameWithoutExtension(FileName);
            Viewer.ShowDialog();
        }

        /// <summary>
        /// Status received from tester
        /// </summary>
        /// <param name="sender">Tester that received the status</param>
        /// <param name="CurrentStatus">Current tester status</param>
        private void Tester_OnReceivedStatus(object sender, Status CurrentStatus)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, Status>(Tester_OnReceivedStatus), sender, CurrentStatus);
                return;
            }

            string StatusText = string.Format("Air temp={0}°F, Coolant temp={1}°F, Eng speed={2}RPM, Throttle={3}% PG Angle={4}°, Vacuum={5}inHg, Starter motor={6}",
                CurrentStatus.AirTemperature, CurrentStatus.CoolantTemperature, CurrentStatus.EngineSpeed, CurrentStatus.Throttle,
                CurrentStatus.DwellAngle, CurrentStatus.Vacuum, CurrentStatus.StarterMotorOn ? "on" : "off");

            UInt32 AveragePulseWidth = (UInt32)((CurrentStatus.PulseWidth_I + CurrentStatus.PulseWidth_II + CurrentStatus.PulseWidth_III + CurrentStatus.PulseWidth_IV) / 4.0);

            StatusText += Environment.NewLine + string.Format("Fuel pump={0}, Pulse widths I={1:N2}ms II={2:N2}ms III={3:N2}ms IV={4:N2}ms Ave={5:N2}ms",
                CurrentStatus.FuelPumpOn ? "on" : "off",
                CurrentStatus.PulseWidth_I / 1000.0, CurrentStatus.PulseWidth_II / 1000.0, CurrentStatus.PulseWidth_III / 1000.0, CurrentStatus.PulseWidth_IV / 1000.0,
                AveragePulseWidth / 1000.0);

            PressureValue.Text = CurrentStatus.Vacuum.ToString();

            if (Tester.IsConnected)
            {
                TesterInfoBox.Text = StatusText;
            }

            if (FirstStatus)
            {
                AirTempInput.Text = CurrentStatus.AirTemperature.ToString();
                CoolantTempInput.Text = CurrentStatus.CoolantTemperature.ToString();
                EngineSpeedInput.Text = CurrentStatus.EngineSpeed.ToString();
                ThrottlePositionInput.Text = CurrentStatus.Throttle.ToString();
                DwellAngleInput.Text = CurrentStatus.DwellAngle.ToString();

                FirstStatus = false;
            }

            if (Recording)
            {
                double Timestamp = (DateTime.Now - RecordingStartTime).TotalMilliseconds;
                Buffer.Add(Timestamp, CurrentStatus);
            }
        }


        /// <summary>
        /// Called when the tester wants to show a message to the user
        /// </summary>
        /// <param name="sender">Tester sending the message</param>
        /// <param name="Message">Message to show</param>
        private void Tester_OnShowMessage(object sender, string Message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<object, string>(Tester_OnShowMessage), sender, Message);
                return;
            }

            OutputBox.AppendText(Message + Environment.NewLine);
        }

        /// <summary>
        /// Updates the user interface with the current settings
        /// </summary>
        private void UpdateUI
            (
            )
        {
            StartSpeedInput.Enabled = SpeedEnable.Checked;
            EndSpeedInput.Enabled = SpeedEnable.Checked;
            StartSpeedLabel1.Enabled = SpeedEnable.Checked;
            StartSpeedLabel2.Enabled = SpeedEnable.Checked;
            EndSpeedLabel1.Enabled = SpeedEnable.Checked;
            EndSpeedLabel2.Enabled = SpeedEnable.Checked;

            StartAirTempInput.Enabled = AirTempEnable.Checked;
            EndAirTempInput.Enabled = AirTempEnable.Checked;
            StartAirTempLabel1.Enabled = AirTempEnable.Checked;
            EndAirTempLabel2.Enabled = AirTempEnable.Checked;
            StartAirTempLabel2.Enabled = AirTempEnable.Checked;
            EndAirTempLabel1.Enabled = AirTempEnable.Checked;

            StartCoolantTempInput.Enabled = CoolantTempEnable.Checked;
            EndCoolantTempInput.Enabled = CoolantTempEnable.Checked;
            StartCoolantTempLabel1.Enabled = CoolantTempEnable.Checked;
            EndCoolantTempLabel1.Enabled = CoolantTempEnable.Checked;
            StartCoolantTempLabel2.Enabled = CoolantTempEnable.Checked;
            EndCoolantTempLabel2.Enabled = CoolantTempEnable.Checked;

            StartStarterInput.Enabled = StarterEnable.Checked;
            EndStarterInput.Enabled = StarterEnable.Checked;
            StartStarterLabel1.Enabled = StarterEnable.Checked;
            EndStarterLabel1.Enabled = StarterEnable.Checked;
            StartStarterLabel2.Enabled = StarterEnable.Checked;
            EndStarterLabel2.Enabled = StarterEnable.Checked;

            DynamicProgressBar.Style = ProgressBarStyle.Continuous;
        }

        /// <summary>
        /// Called when user applies the custom settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomSettingsApplyBtn_Click(object sender, EventArgs e)
        {
            int CoolantTemp = 0;
            int.TryParse(CoolantTempInput.Text, out CoolantTemp);

            int AirTemp = 0;
            int.TryParse(AirTempInput.Text, out AirTemp);

            uint ThrottlePosition = 0;
            uint.TryParse(ThrottlePositionInput.Text, out ThrottlePosition);

            uint DwellAngle = 135;
            uint.TryParse(DwellAngleInput.Text, out DwellAngle);

            uint EngineSpeed = 1200;
            uint.TryParse(EngineSpeedInput.Text, out EngineSpeed);

            if (StarterMotorInput.Checked && (EngineSpeed < 1 || EngineSpeed > 300))
            {
                DialogResult Result = MessageBox.Show("The engine speed does not match a running starter motor. Are you sure you wish to use these settings?",
                    Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Result != DialogResult.OK)
                {
                    return;
                }
            }

            Tester.SetCoolantTemperature(CoolantTemp);
            Tester.SetAirTemperature(AirTemp);
            Tester.SetThrottle(ThrottlePosition);
            Tester.SetDwellAngle(DwellAngle);
            Tester.SetEngineSpeed(EngineSpeed);
            Tester.SetStarterMotorState(StarterMotorInput.Checked);
        }

        /// <summary>
        /// Called when user clicks on the copy button
        /// Copies the current tester status to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyInfoBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TesterInfoBox.Text);
        }

        /// <summary>
        /// Called when user clicks on apply to set a preset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyPresetBtn_Click(object sender, EventArgs e)
        {
            string Preset = (string)PresetSelector.SelectedItem;

            if (Preset == "Engine Off") Tester.UsePreset_EngineOff();
            else if (Preset == "Cranking") Tester.UsePreset_Cranking();
            else if (Preset == "Cold Idle") Tester.UsePreset_ColdIdle();
            else if (Preset == "Hot Idle") Tester.UsePreset_HotIdle();
            else if (Preset == "Cruise 30MPH") Tester.UsePreset_Cruise30MPH();
            else if (Preset == "Cruise 70MPH") Tester.UsePreset_Cruise70MPH();
            else if (Preset == "Gentle Acceleration") Tester.UsePreset_GentleAcceleration();
            else if (Preset == "Moderate Acceleration") Tester.UsePreset_ModerateAcceleration();
            else if (Preset == "Hard Acceleration") Tester.UsePreset_HardAcceleration();
            else if (Preset == "Cranking (Unstable RPM)") Tester.UsePreset_UnstableCranking();
        }

        /// <summary>
        /// Starts the dynamic simulation
        /// </summary>
        private void StartDynamic
            (
            )
        {
            DynamicSettings Settings = new DynamicSettings();

            if (!uint.TryParse(DynamicTimePeriod.Text, out Settings.Duration))
            {
                MessageBox.Show("Invalid period for dynamic test", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Settings.Duration < 100)
            {
                MessageBox.Show("Dynamic test must be at least 100ms long", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!uint.TryParse(DynamicResolutionInput.Text, out Settings.Resolution))
            {
                Settings.Resolution = 10;
            }

            Settings.UseSpeed = SpeedEnable.Checked;
            Settings.UseAirTemp = AirTempEnable.Checked;
            Settings.UseCoolantTemp = CoolantTempEnable.Checked;
            Settings.UseStarter = StarterEnable.Checked;

            Settings.StartSpeed = 0;
            Settings.EndSpeed = 0;
            Settings.StartAirTemp = 0;
            Settings.EndAirTemp = 0;
            Settings.StartCoolantTemp = 0;
            Settings.EndCoolantTemp = 0;
            Settings.StartStarter = 0;
            Settings.EndStarter = 0;

            if (SpeedEnable.Checked)
            {
                if (!uint.TryParse(StartSpeedInput.Text, out Settings.StartSpeed)) Settings.UseSpeed = false;
                if (!uint.TryParse(EndSpeedInput.Text, out Settings.EndSpeed)) Settings.UseSpeed = false;
            }

            if (AirTempEnable.Checked)
            {
                if (!int.TryParse(StartAirTempInput.Text, out Settings.StartAirTemp)) Settings.UseAirTemp = false;
                if (!int.TryParse(EndAirTempInput.Text, out Settings.EndAirTemp)) Settings.UseAirTemp = false;
            }

            if (CoolantTempEnable.Checked)
            {
                if (!int.TryParse(StartCoolantTempInput.Text, out Settings.StartCoolantTemp)) Settings.UseCoolantTemp = false;
                if (!int.TryParse(EndCoolantTempInput.Text, out Settings.EndCoolantTemp)) Settings.UseCoolantTemp = false;
            }

            if (StarterEnable.Checked)
            {
                if (!uint.TryParse(StartStarterInput.Text, out Settings.StartStarter)) Settings.UseStarter = false;
                if (!uint.TryParse(EndStarterInput.Text, out Settings.EndStarter)) Settings.UseStarter = false;
            }

            if (Settings.StartStarter > Settings.EndStarter)
            {
                MessageBox.Show("Starter motor off time must be later than the on time", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Tester.StartDynamicTest(Settings);
        }

        /// <summary>
        /// Called when user clicks on the engine test button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineTestBtn_Click(object sender, EventArgs e)
        {
            Tester.EngineTest();
        }

        private void DynamicStartStopBtn_Click(object sender, EventArgs e)
        {
            if (Tester.DynamicTestRunning)
            {
                Tester.StopDynamicTest();
            }
            else
            {
                StartDynamic();
            }
        }

        private void SpeedEnable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void AirTempEnable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void CoolantTempEnable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void StarterEnable_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
