using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DJetronicStudio
{
    public class MPSProfile
    {
        public enum CalibrationTypes { Factory, Inductance, WidebandO2, TuneOMatic };

        public CalibrationTypes CalibrationType;
        public string Name;
        public string Description;
        public double AtmosphericPressure;
        public DateTime CreationDate;
        public double[] PulseWidths;

        public MPSProfile
            (
            ) : this("Untitled", "", CalibrationTypes.TuneOMatic, DateTime.Now, 0)
        {
        }

        public MPSProfile
            (
            string Name,
            string Description,
            CalibrationTypes CalibrationType,
            DateTime CreationDate,
            double AtmosphericPressure
            )
        {
            this.Name = Name;
            this.Description = Description;
            this.CalibrationType = CalibrationType;
            this.AtmosphericPressure = AtmosphericPressure;
            this.CreationDate = CreationDate;

            PulseWidths = new double[16];
        }

        /// <summary>
        /// Stores the profile in a file
        /// </summary>
        /// <param name="FileName">Path and name of file</param>
        public void WriteToFile
            (
            string FileName
            )
        {
            // fixme - to do
        }

        /// <summary>
        /// Creates a profile from a file
        /// </summary>
        /// <param name="FileName">Path and name of file</param>
        /// <returns>The created profile</returns>
        public static MPSProfile ReadFromFile
            (
            string FileName
            )
        {
            MPSProfile Profile = new MPSProfile();

            // fixme - to do

            return Profile;
        }
    }
}
