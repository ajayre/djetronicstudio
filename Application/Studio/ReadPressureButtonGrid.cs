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
                    (Ctrl as ReadPressureButton).Click += Btn_Click;
                }
            }

            ResetAll();
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
        /// Called when a button is clicked
        /// Raises an event passing the pressure of the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, EventArgs e)
        {
            ReadPressureButton PButton = sender as ReadPressureButton;

            if (OnButtonClicked != null) OnButtonClicked(PButton, PButton.Pressure, PButton.Done);

            PButton.Done = true;
        }
    }
}
