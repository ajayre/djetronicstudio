﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJetronicStudio
{
    public class SimSettings
    {
        public enum ResolutionUnits
        {
            Milliseconds,
            Microseconds
        }

        public uint TotalTimeMs = 200;
        public uint Resolution = 8;
        public ResolutionUnits ResolutionUnit = ResolutionUnits.Microseconds;
        public uint EngineSpeedRpm = 1200;
        public double StartAirTemperatureF = 32;
        public double EndAirTemperatureF = 32;
        public double StartCoolantTemperatureF = 100;
        public double EndCoolantTemperatureF = 100;
        public uint StarterMotorOnMs = 0;
        public uint StarterMotorOffMs = 0;
        public uint StartThrottlePcent = 0;
        public uint EndThrottlePcent = 0;
        public uint StartManifoldVacuumInhg = 15;
        public uint EndManifoldVacuumInhg = 15;
        public uint StartRheostat = 11;
        public uint EndRheostat = 11;
        public double StartBatteryVoltage = 13.5;
        public double EndBatteryVoltage = 13.5;
        public uint DwellAngle = 135;
    }
}
