﻿using System;
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

        private List<ToolbarButton> ToolbarButtons = new List<ToolbarButton>();
        private List<StatusLabel> StatusLabels = new List<StatusLabel>();
        private TuneOMatic Tuner;
        private bool Recording;
        private DateTime RecordingStartTime;
        private List<Tuple<double, double, UInt16>> RecordingBuffer = new List<Tuple<double, double, UInt16>>();
        private double LastPressure;

        public TuneOMaticUI
            (
            TuneOMatic Tuner
            )
        {
            this.Tuner = Tuner;

            InitializeComponent();

            Tuner.OnReceivedPressure += Tuner_OnReceivedPressure;
            Tuner.OnReceivedPulseWidth += Tuner_OnReceivedPulseWidth;
            Recording = false;

            ShowInitialSettings();

            UpdateUI();
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
    }
}