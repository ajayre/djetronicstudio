using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DJetronicStudio
{
    public partial class ReadPressureButtonGrid : UserControl
    {
        public delegate void OnButtonClickedHandler(ReadPressureButton sender, int Pressure, bool PreviouslyClicked);
        public event OnButtonClickedHandler OnButtonClicked = null;

        public ReadPressureButtonGrid()
        {
            InitializeComponent();

            foreach (Control Ctrl in Controls)
            {
                if (Ctrl is ReadPressureButton)
                {
                    (Ctrl as ReadPressureButton).OnClick += ReadPressureButtonGrid_OnClick;
                }
            }

            ResetAll();
        }

        /// <summary>
        /// Called when a button is clicked
        /// Raises an event passing the pressure of the button
        /// </summary>
        /// <param name="sender"></param>
        private void ReadPressureButtonGrid_OnClick(object sender)
        {
            ReadPressureButton PButton = sender as ReadPressureButton;

            if (OnButtonClicked != null) OnButtonClicked(PButton, PButton.Pressure, PButton.Done);

            PButton.Done = true;
        }

        /// <summary>
        /// Resets all buttons so they show 'not done'
        /// </summary>
        public void ResetAll
            (
            )
        {
            foreach (Control Ctrl in Controls)
            {
                if (Ctrl is ReadPressureButton)
                {
                    (Ctrl as ReadPressureButton).Done = false;
                }
            }
        }

        /// <summary>
        /// Checks if all pressure buttons have been clicked on
        /// </summary>
        /// <returns></returns>
        public bool AreAllDone
            (
            )
        {
            foreach (Control Ctrl in Controls)
            {
                if (Ctrl is ReadPressureButton)
                {
                    if (!(Ctrl as ReadPressureButton).Done) return false;
                }
            }

            return true;
        }
    }
}
