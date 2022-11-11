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
    public partial class ReadPressureButton : UserControl
    {
        private int _Pressure = 0;
        public int Pressure
        {
            get { return _Pressure; }
            set
            {
                _Pressure = value;
                UpdateButtonText();
            }
        }

        private bool _Done = false;
        public bool Done
        {
            get { return _Done; }
            set
            {
                _Done = value;
                if (_Done)
                {
                    Btn.Image = Properties.Resources.check_mark_24;
                }
                else
                {
                    Btn.Image = null;
                }
                UpdateButtonText();
            }
        }

        public ReadPressureButton()
        {
            InitializeComponent();

            _Pressure = 0;
            _Done = false;
            UpdateButtonText();
        }

        /// <summary>
        /// Updates the button text based on the current settings
        /// </summary>
        private void UpdateButtonText
            (
            )
        {
            if (_Done)
            {
                Btn.Text = string.Format("      Vacuum set to {0} inHg", _Pressure);
            }
            else
            {
                Btn.Text = string.Format("Vacuum set to {0} inHg", _Pressure);
            }
        }

        /// <summary>
        /// Called when button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_MouseClick(object sender, MouseEventArgs e)
        {
            Done = true;
        }
    }
}
