using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DJetronicStudio
{
    public class ToolbarButton
    {
        public EventHandler ButtonAction;
        public string Description;
        public Image ButtonImage;
        public bool Enabled;

        public ToolbarButton
            (
            string Description,
            Image ButtonImage,
            EventHandler ButtonAction,
            bool Enabled
            )
        {
            this.Description = Description;
            this.ButtonImage = ButtonImage;
            this.ButtonAction = ButtonAction;
            this.Enabled = Enabled;
        }
    }
}
