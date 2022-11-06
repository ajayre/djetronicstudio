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
    public partial class WizardBanner : UserControl
    {
        /// <summary>
        /// The title to display on the banner
        /// </summary>
        public string Title
        {
            get { return TitleText.Text; }
            set { TitleText.Text = value; }
        }

        public WizardBanner()
        {
            InitializeComponent();
        }
    }
}
