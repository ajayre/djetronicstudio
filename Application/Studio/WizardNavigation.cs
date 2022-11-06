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
    public partial class WizardNavigation : UserControl
    {
        public delegate void OnCancelHandler(object sender);
        public event OnCancelHandler OnCancel = null;

        public delegate void OnPreviousHandler(object sender);
        public event OnPreviousHandler OnPrevious = null;

        public delegate void OnNextHandler(object sender);
        public event OnNextHandler OnNext = null;

        private bool _FirstPage;
        public bool FirstPage
        {
            get { return _FirstPage; }
            set
            {
                _FirstPage = value;
                if (_FirstPage)
                    PreviousBtn.Enabled = false;
                else
                    PreviousBtn.Enabled = true;
            }
        }

        private bool _LastPage;
        public bool LastPage
        {
            get { return _LastPage; }
            set
            {
                _LastPage = value;
                if (_LastPage)
                    NextBtn.Text = "Finish";
                else
                    NextBtn.Text = "Next >";
            }
        }

        public WizardNavigation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when user clicks on the cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            if (OnCancel != null) OnCancel(this);
        }

        /// <summary>
        /// Called when user clicks on the previous button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousBtn_Click(object sender, EventArgs e)
        {
            if (OnPrevious != null) OnPrevious(this);
        }

        /// <summary>
        /// Called when user clicks on the next button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextBtn_Click(object sender, EventArgs e)
        {
            if (OnNext != null) OnNext(this);
        }
    }
}
