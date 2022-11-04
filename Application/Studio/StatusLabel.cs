using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DJetronicStudio
{
    public class StatusLabel
    {
        public string Text;
        public Image StatusImage;
        public int Width;

        public StatusLabel
            (
            string Text,
            Image StatusImage,
            int Width
            )
        {
            this.Text = Text;
            this.StatusImage = StatusImage;
            this.Width = Width;
        }
    }
}
