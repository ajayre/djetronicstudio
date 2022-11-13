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
    public partial class EditMPSForm : Form
    {
        public string NameText
        {
            get { return NameInput.Text; }
            set { NameInput.Text = value; }
        }

        public string DescriptionText
        {
            get { return DescriptionInput.Text; }
            set { DescriptionInput.Text = value; }
        }

        public EditMPSForm()
        {
            InitializeComponent();
        }
    }
}
