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

        public bool ShowAirTemperature = false;
        public bool ShowCoolantTemperature = false;

        public bool ShowTPSFullThrottle = false;
        public bool ShowTPSIdle = false;
        public bool ShowTPSAcceleration1 = false;
        public bool ShowTPSAcceleration2 = false;

        public bool ShowPulseGeneratorGroupI = false;
        public bool ShowPulseGeneratorGroupII = false;
        public bool ShowPulseGeneratorGroupIII = false;
        public bool ShowPulseGeneratorGroupIV = false;

        public bool ShowStart = false;
        public bool ShowFuelPumpRelay = false;

        public bool ShowDiagI_III = false;

        public bool ShowCustom = false;
        public string CustomVectorName = null;

        public Color InjectorGroupIColor = Color.Red;
        public Color InjectorGroupIIColor = Color.Green;
        public Color InjectorGroupIIIColor = Color.Blue;
        public Color InjectorGroupIVColor = Color.Orange;

        public Color MPSPin7Color = Color.Purple;
        public Color MPSPin8Color = Color.Cyan;
        public Color MPSPin10Color = Color.Magenta;
        public Color MPSPin15Color = Color.LimeGreen;

        public Color TPSFullThrottleColor = Color.Khaki;
        public Color TPSIdleColor = Color.Lavender;
        public Color TPSAcceleration1Color = Color.Gold;
        public Color TPSAcceleration2Color = Color.CadetBlue;

        public Color PulseGeneratorGroupIColor = Color.DarkRed;
        public Color PulseGeneratorGroupIIColor = Color.DarkGreen;
        public Color PulseGeneratorGroupIIIColor = Color.DarkBlue;
        public Color PulseGeneratorGroupIVColor = Color.DarkOrange;

        public Color StartColor = Color.BurlyWood;
        public Color FuelPumpRelayColor = Color.BlueViolet;

        public Color AirTemperatureColor = Color.Tomato;
        public Color CoolantTemperatureColor = Color.Teal;

        public Color DiagI_IIIColor = Color.SlateBlue;

        public Color CustomColor = Color.Pink;
    }
}
