﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace DJetronicStudio
{
    public class MPSProfile
    {
        public const int MAX_VACUUM = 15;
        // determined experimentally
        public const double CORRECTION_FACTOR = 0.03;

        public enum CalibrationTypes { Factory, Inductance, WidebandO2, TuneOMatic, None };

        public CalibrationTypes CalibrationType;
        public string Name;
        public string Description;
        public double AtmosphericPressure;
        public DateTime CreationDate;
        public double[] PulseWidths;
        public bool UserProfile;
        public string FileName;

        public MPSProfile
            (
            ) : this("Untitled", "", CalibrationTypes.TuneOMatic, DateTime.Now, 0, true)
        {
        }

        public MPSProfile
            (
            string Name,
            string Description,
            CalibrationTypes CalibrationType,
            DateTime CreationDate,
            double AtmosphericPressure,
            bool UserProfile
            )
        {
            this.Name = Name;
            this.Description = Description;
            this.CalibrationType = CalibrationType;
            this.AtmosphericPressure = AtmosphericPressure;
            this.CreationDate = CreationDate;
            this.UserProfile = UserProfile;

            PulseWidths = new double[MAX_VACUUM + 1];
        }

        public override string ToString()
        {
            return string.Format("{0} [{1}, {2} Calibration]", Name, UserProfile ? "User Profile" : "Internal Profile", CalibrationType);
        }

        /// <summary>
        /// Clears the pulse widths
        /// </summary>
        public void ClearPulseWidths
            (
            )
        {
            for (int p = 0; p <= MAX_VACUUM; p++) PulseWidths[p] = 0;
        }

        /// <summary>
        /// Gets the pulse widths adjusted for a specific atmpspheric pressure
        /// </summary>
        /// <param name="AtmosphericPressure">Atmospheric pressure to adjust to</param>
        public double[] GetAdjustedPulseWidths
            (
            double AtmosphericPressure,
            double CorrectionFactor
            )
        {
            double[] AdjPulseWidths = new double[MAX_VACUUM + 1];

            for (int p = 0; p <= MAX_VACUUM; p++)
            {
                // see: https://www.benzworld.org/threads/atmospheric-pressure-testing-of-d-jetronic-manifold-pressure-sensor-mps.3109384/#post-18503957

                double LineSlope = (PulseWidths[p] - 19.8823805648711) / -3.43310224625425 / 100000;
                LineSlope += CORRECTION_FACTOR;
                double PAchange = AtmosphericPressure - this.AtmosphericPressure;
                double PWchange = (LineSlope + CorrectionFactor) * PAchange;
                AdjPulseWidths[p] = PulseWidths[p] + PWchange;
            }

            return AdjPulseWidths;
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
            using (XmlTextWriter Writer = new XmlTextWriter(FileName, Encoding.UTF8))
            {
                // formatting to use
                Writer.Formatting = Formatting.Indented;
                Writer.Indentation = 2;

                Writer.WriteStartDocument();
                Writer.WriteStartElement("MPS");

                new XElement("Name", Name).WriteTo(Writer);
                new XElement("Description", Description).WriteTo(Writer);
                new XElement("CalibrationType", CalibrationType).WriteTo(Writer);
                new XElement("AtmosphericPressure", AtmosphericPressure).WriteTo(Writer);
                new XElement("CreationDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).WriteTo(Writer);

                XElement PulseWidthsEle = new XElement("PulseWidths");
                for (int Vac = 0; Vac <= MAX_VACUUM; Vac++)
                {
                    XElement VacEle = new XElement(string.Format("Vacuum_{0}", Vac), PulseWidths[Vac]);
                    PulseWidthsEle.Add(VacEle);
                }

                PulseWidthsEle.WriteTo(Writer);

                Writer.WriteEndElement();
                Writer.WriteEndDocument();
                Writer.Close();
            }
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

            string Xml = File.ReadAllText(FileName, Encoding.UTF8);

            using (StringReader StrReader = new StringReader(Xml))
            {
                using (XmlReader Reader = new XmlTextReader(StrReader))
                {
                    XElement Root = XElement.Load(Reader);

                    Profile.Name = Root.Element("Name").Value.Trim();
                    Profile.Description = Root.Element("Description").Value.Trim();
                    Profile.AtmosphericPressure = (double)Root.Element("AtmosphericPressure");
                    Profile.CalibrationType = (CalibrationTypes)Enum.Parse(typeof(CalibrationTypes), Root.Element("CalibrationType").Value.Trim());
                    Profile.CreationDate = DateTime.Parse(Root.Element("CreationDate").Value.Trim());
                    Profile.FileName = FileName;

                    Regex VacName = new Regex("^Vacuum_([0-9]+)$", RegexOptions.IgnoreCase);

                    foreach (XElement Element in Root.Element("PulseWidths").Nodes().OfType<XElement>())
                    {
                        Match VacMatch = VacName.Match(Element.Name.ToString());
                        if (VacMatch.Success)
                        {
                            int Vacuum = int.Parse(VacMatch.Groups[1].Value.Trim());
                            if ((Vacuum >= 0) && (Vacuum <= MAX_VACUUM))
                            {
                                Profile.PulseWidths[Vacuum] = double.Parse(Element.Value.Trim());
                            }
                        }
                    }
                }
            }

            return Profile;
        }
    }
}
