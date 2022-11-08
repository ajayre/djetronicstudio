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
    public partial class MPSProfileUI : UserControl
    {
        private MPSProfile _Profile;
        public MPSProfile Profile
        {
            get { return _Profile; }
            set
            {
                _Profile = value;

                MPSNamelabel.Text = _Profile.Name;

                if (_Profile.CalibrationType == MPSProfile.CalibrationTypes.Factory)
                {
                    CalibrationIcon.Visible = true;
                }
                else
                {
                    CalibrationIcon.Visible = false;
                }

                RenameBtn.Enabled = _Profile.UserProfile;
                DeleteBtn.Enabled = _Profile.UserProfile;
            }
        }

        public MPSProfileUI()
        {
            InitializeComponent();
        }
    }
}
