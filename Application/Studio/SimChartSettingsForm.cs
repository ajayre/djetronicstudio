using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJetronicStudio
{
    public partial class SimChartSettingsForm : Form
    {
        public SimChartSettings Settings;

        private bool DisableEvents;

        public SimChartSettingsForm()
        {
            InitializeComponent();

            DisableEvents = false;
        }

        /// <summary>
        /// Called when form is shown, shows the current settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimChartSettingsForm_Shown(object sender, EventArgs e)
        {
            ShowSettings();
        }

        /// <summary>
        /// Called when user closes the form
        /// If the user clicked on OK then applies the settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SimChartSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                ApplySettings();
            }
        }

        /// <summary>
        /// Shows the current settings
        /// </summary>
        private void ShowSettings
            (
            )
        {
            if (Settings == null) return;

            try
            {
                DisableEvents = true;

                InjectorGroupIInput.Checked = Settings.ShowInjectorGroupI;
                InjectorGroupIIInput.Checked = Settings.ShowInjectorGroupII;
                InjectorGroupIIIInput.Checked = Settings.ShowInjectorGroupIII;
                InjectorGroupIVInput.Checked = Settings.ShowInjectorGroupIV;

                InjectorGroupIColorInput.BackColor = Settings.InjectorGroupIColor;
                InjectorGroupIIColorInput.BackColor = Settings.InjectorGroupIIColor;
                InjectorGroupIIIColorInput.BackColor = Settings.InjectorGroupIIIColor;
                InjectorGroupIVColorInput.BackColor = Settings.InjectorGroupIVColor;

                MPSPin7Input.Checked = Settings.ShowMPSPin7;
                MPSPin8Input.Checked = Settings.ShowMPSPin8;
                MPSPin10Input.Checked = Settings.ShowMPSPin10;
                MPSPin15Input.Checked = Settings.ShowMPSPin15;

                MPSPin7ColorInput.BackColor = Settings.MPSPin7Color;
                MPSPin8ColorInput.BackColor = Settings.MPSPin8Color;
                MPSPin10ColorInput.BackColor = Settings.MPSPin10Color;
                MPSPin15ColorInput.BackColor = Settings.MPSPin15Color;

                CustomInput.Checked = Settings.ShowCustom;
                CustomVectorNameInput.Text = Settings.CustomVectorName;
                CustomColorInput.BackColor = Settings.CustomColor;
                CustomColorInput.Tag = CustomColorInput.BackColor;

                UpdateUI();
            }
            finally
            {
                DisableEvents = false;
            }
        }

        /// <summary>
        /// Applies the current settings
        /// </summary>
        private void ApplySettings
            (
            )
        {
            Settings.ShowInjectorGroupI = InjectorGroupIInput.Checked;
            Settings.ShowInjectorGroupII = InjectorGroupIIInput.Checked;
            Settings.ShowInjectorGroupIII = InjectorGroupIIIInput.Checked;
            Settings.ShowInjectorGroupIV = InjectorGroupIVInput.Checked;

            Settings.InjectorGroupIColor = InjectorGroupIColorInput.BackColor;
            Settings.InjectorGroupIIColor = InjectorGroupIIColorInput.BackColor;
            Settings.InjectorGroupIIIColor = InjectorGroupIIIColorInput.BackColor;
            Settings.InjectorGroupIVColor = InjectorGroupIVColorInput.BackColor;

            Settings.ShowMPSPin7 = MPSPin7Input.Checked;
            Settings.ShowMPSPin8 = MPSPin8Input.Checked;
            Settings.ShowMPSPin10 = MPSPin10Input.Checked;
            Settings.ShowMPSPin15 = MPSPin15Input.Checked;

            Settings.MPSPin7Color = MPSPin7ColorInput.BackColor;
            Settings.MPSPin8Color = MPSPin8ColorInput.BackColor;
            Settings.MPSPin10Color = MPSPin10ColorInput.BackColor;
            Settings.MPSPin15Color = MPSPin15ColorInput.BackColor;

            Settings.ShowCustom = CustomInput.Checked;
            Settings.CustomVectorName = CustomVectorNameInput.Text;
            Settings.CustomColor = CustomColorInput.BackColor;
        }

        /// <summary>
        /// Called when user wants to choose a new color for a waveform
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorInput_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog.Color = (sender as Panel).BackColor;
            if (ColorDialog.ShowDialog() == DialogResult.OK)
            {
                (sender as Panel).BackColor = ColorDialog.Color;
            }
        }

        /// <summary>
        /// Called when user changes the state of a checkbox
        /// Updates the user interface to match
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_CheckedChanged(object sender, EventArgs e)
        {
            if (!DisableEvents)
            {
                UpdateUI();
            }
        }

        /// <summary>
        /// Updates the state of the user interface to show the correct
        /// enabled/disabled state
        /// </summary>
        private void UpdateUI
            (
            )
        {
            CustomVectorNameInput.Enabled = CustomInput.Checked;
            CustomColorInput.Enabled = CustomInput.Checked;

            if (CustomColorInput.Enabled)
            {
                CustomColorInput.BackColor = (Color)CustomColorInput.Tag;
                CustomColorInput.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                CustomColorInput.Tag = CustomColorInput.BackColor;
                CustomColorInput.BackColor = Color.LightGray;
                CustomColorInput.BorderStyle = BorderStyle.None;
            }
        }
    }
}
