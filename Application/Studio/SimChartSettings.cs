using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DJetronicStudio
{
    public class SimChartSettings
    {
        public bool ShowInjectorGroupI = false;
        public bool ShowInjectorGroupII = false;
        public bool ShowInjectorGroupIII = false;
        public bool ShowInjectorGroupIV = false;

        public bool ShowMPSPin7 = false;
        public bool ShowMPSPin8 = false;
        public bool ShowMPSPin10 = false;
        public bool ShowMPSPin15 = false;

        public bool ShowCustom = false;
        public string CustomVectorName = null;

        public Color InjectorGroupIColor = Color.Red;
        public Color InjectorGroupIIColor = Color.Green;
        public Color InjectorGroupIIIColor = Color.Blue;
        public Color InjectorGroupIVColor = Color.Purple;

        public Color MPSPin7Color = Color.Orange;
        public Color MPSPin8Color = Color.Cyan;
        public Color MPSPin10Color = Color.Magenta;
        public Color MPSPin15Color = Color.LimeGreen;

        public Color CustomColor = Color.Pink;
    }
}
