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
        public enum ButtonTypes { Edit, TuneUsing, Delete, Export };

        public delegate void OnButtonClickedHandler(object sender, ButtonTypes ButtonType, MPSProfile Profile);
        public event OnButtonClickedHandler OnButtonClicked = null;

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

                EditBtn.Enabled = _Profile.UserProfile;
                DeleteBtn.Enabled = _Profile.UserProfile;

                ToolTips.SetToolTip(Picture, _Profile.Description);
            }
        }

        public MPSProfileUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Refreshes the display of the profile name
        /// </summary>
        public void RefreshName
            (
            )
        {
            MPSNamelabel.Text = _Profile.Name;
        }

        /// <summary>
        /// Refreshes the display of the profile description
        /// </summary>
        public void RefreshDescription
            (
            )
        {
            ToolTips.SetToolTip(Picture, _Profile.Description);
        }

        /// <summary>
        /// Called when user clicks on the rename button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (OnButtonClicked != null) OnButtonClicked(this, ButtonTypes.Edit, _Profile);
        }

        /// <summary>
        /// Called when user clicks on the tune using button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TuneUsingBtn_Click(object sender, EventArgs e)
        {
            if (OnButtonClicked != null) OnButtonClicked(this, ButtonTypes.TuneUsing, _Profile);
        }

        /// <summary>
        /// Called when user clicks on the export button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (OnButtonClicked != null) OnButtonClicked(this, ButtonTypes.Export, _Profile);
        }

        /// <summary>
        /// Called when user clicks on the delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (OnButtonClicked != null) OnButtonClicked(this, ButtonTypes.Delete, _Profile);
        }
    }
}
