using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJetronicStudio
{
    public class DynamicSettings
    {
        public const uint INVALID_STARTER_TIME = 0xFFFF;

        public uint Duration;
        public uint Resolution;

        public bool UseSpeed;
        public bool UseAirTemp;
        public bool UseCoolantTemp;
        public bool UseStarter;
        public bool UseThrottle;

        public uint StartSpeed;
        public uint EndSpeed;
        public int StartAirTemp;
        public int EndAirTemp;
        public int StartCoolantTemp;
        public int EndCoolantTemp;
        public uint StartStarter;
        public uint EndStarter;
        public uint StartThrottle;
        public uint EndThrottle;

        /// <summary>
        /// Converts the settings into a lookup table
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateTable
            (
            )
        {
            uint Steps = Duration / Resolution;
            int SpeedStep = 0;
            int AirTempStep = 0;
            int CoolantTempStep = 0;
            int ThrottleStep = 0;

            if (UseSpeed)
            {
                if (EndSpeed > StartSpeed)
                {
                    SpeedStep = (int)(((double)EndSpeed - (double)StartSpeed + 1) * 10.0 / Steps);
                }
                else if (StartSpeed > EndSpeed)
                {
                    SpeedStep = -(int)(((double)StartSpeed - (double)EndSpeed + 1) * 10.0 / Steps);
                }
            }

            if (UseAirTemp)
            {
                if (EndAirTemp > StartAirTemp)
                {
                    AirTempStep = (int)(((double)EndAirTemp - (double)StartAirTemp + 1) * 100 / Steps);
                }
                else if (StartAirTemp > EndAirTemp)
                {
                    AirTempStep = -(int)(((double)StartAirTemp - (double)EndAirTemp + 1) * 100 / Steps);
                }
            }

            if (UseCoolantTemp)
            {
                if (EndCoolantTemp > StartCoolantTemp)
                {
                    CoolantTempStep = (int)(((double)EndCoolantTemp - (double)StartCoolantTemp + 1) * 100 / Steps);
                }
                else if (StartAirTemp > EndAirTemp)
                {
                    CoolantTempStep = -(int)(((double)StartCoolantTemp - (double)EndCoolantTemp + 1) * 100 / Steps);
                }
            }

            if (UseThrottle)
            {
                if (EndThrottle > StartThrottle)
                {
                    ThrottleStep = (int)(((double)EndThrottle - (double)StartThrottle + 1) * 100 / Steps);
                }
                else if (StartThrottle > EndThrottle)
                {
                    ThrottleStep = -(int)(((double)StartThrottle - (double)EndThrottle + 1) * 100 / Steps);
                }
            }

            if (!UseStarter)
            {
                StartStarter = INVALID_STARTER_TIME;
                EndStarter = INVALID_STARTER_TIME;
            }

            byte[] Table = new byte[48];

            Table[0] = (byte)(Steps & 0x7F);
            Table[1] = (byte)((Steps >> 7) & 0x01);
            Table[2] = (byte)((Steps >> 8) & 0x7F);
            Table[3] = (byte)((Steps >> 15) & 0x01);

            Table[4] = (byte)(Resolution & 0x7F);
            Table[5] = (byte)((Resolution >> 7) & 0x01);
            Table[6] = (byte)((Resolution >> 8) & 0x7F);
            Table[7] = (byte)((Resolution >> 15) & 0x01);

            Table[8] = (byte)(StartSpeed & 0x7F);
            Table[9] = (byte)((StartSpeed >> 7) & 0x01);
            Table[10] = (byte)((StartSpeed >> 8) & 0x7F);
            Table[11] = (byte)((StartSpeed >> 15) & 0x01);

            Table[12] = (byte)(SpeedStep & 0x7F);
            Table[13] = (byte)((SpeedStep >> 7) & 0x01);
            Table[14] = (byte)((SpeedStep >> 8) & 0x7F);
            Table[15] = (byte)((SpeedStep >> 15) & 0x01);

            Table[16] = (byte)(StartAirTemp & 0x7F);
            Table[17] = (byte)((StartAirTemp >> 7) & 0x01);
            Table[18] = (byte)((StartAirTemp >> 8) & 0x7F);
            Table[19] = (byte)((StartAirTemp >> 15) & 0x01);

            Table[20] = (byte)(AirTempStep & 0x7F);
            Table[21] = (byte)((AirTempStep >> 7) & 0x01);
            Table[22] = (byte)((AirTempStep >> 8) & 0x7F);
            Table[23] = (byte)((AirTempStep >> 15) & 0x01);

            Table[24] = (byte)(StartCoolantTemp & 0x7F);
            Table[25] = (byte)((StartCoolantTemp >> 7) & 0x01);
            Table[26] = (byte)((StartCoolantTemp >> 8) & 0x7F);
            Table[27] = (byte)((StartCoolantTemp >> 15) & 0x01);

            Table[28] = (byte)(CoolantTempStep & 0x7F);
            Table[29] = (byte)((CoolantTempStep >> 7) & 0x01);
            Table[30] = (byte)((CoolantTempStep >> 8) & 0x7F);
            Table[31] = (byte)((CoolantTempStep >> 15) & 0x01);

            Table[32] = (byte)(StartStarter & 0x7F);
            Table[33] = (byte)((StartStarter >> 7) & 0x01);
            Table[34] = (byte)((StartStarter >> 8) & 0x7F);
            Table[35] = (byte)((StartStarter >> 15) & 0x01);

            Table[36] = (byte)(EndStarter & 0x7F);
            Table[37] = (byte)((EndStarter >> 7) & 0x01);
            Table[38] = (byte)((EndStarter >> 8) & 0x7F);
            Table[39] = (byte)((EndStarter >> 15) & 0x01);

            Table[40] = (byte)(StartThrottle & 0x7F);
            Table[41] = (byte)((StartThrottle >> 7) & 0x01);
            Table[42] = (byte)((StartThrottle >> 8) & 0x7F);
            Table[43] = (byte)((StartThrottle >> 15) & 0x01);

            Table[44] = (byte)(ThrottleStep & 0x7F);
            Table[45] = (byte)((ThrottleStep >> 7) & 0x01);
            Table[46] = (byte)((ThrottleStep >> 8) & 0x7F);
            Table[47] = (byte)((ThrottleStep >> 15) & 0x01);

            return Table;
        }
    }
}
