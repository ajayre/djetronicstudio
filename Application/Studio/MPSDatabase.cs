using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace DJetronicStudio
{
    public class MPSDatabase
    {
        private string InternalFolder;
        private string UserFolder;
        private List<MPSProfile> Profiles = new List<MPSProfile>();

        public MPSDatabase
            (
            )
        {
#if DEBUG
            InternalFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\..\..\..\MPS Database";
#else
            InternalFolder = Path.GetDirectoryName(Application.ExecutablePath) + @"\MPS Database";
#endif

            UserFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + Application.ProductName + Path.DirectorySeparatorChar + "MPS Database";

            if (!Directory.Exists(UserFolder))
            {
                Directory.CreateDirectory(UserFolder);
            }

            Load(InternalFolder, false);
            Load(UserFolder, true);
        }

        /// <summary>
        /// Reloads the MPS profiles
        /// </summary>
        public void Reload
            (
            )
        {
            Profiles.Clear();

            Load(InternalFolder, false);
            Load(UserFolder, true);
        }

        /// <summary>
        /// Loads MPS profiles from a folder
        /// </summary>
        /// <param name="Folder">Path to folder</param>
        /// <param name="UserProfile">true if this is a user-defined profile</param>
        private void Load
            (
            string Folder,
            bool UserProfile
            )
        {
            string[] FileNames = Directory.GetFiles(Folder, "*.mps");

            foreach (string FileName in FileNames)
            {
                try
                {
                    MPSProfile Profile = MPSProfile.ReadFromFile(FileName);
                    Profile.UserProfile = UserProfile;

                    Profiles.Add(Profile);
                }
                catch (Exception Exc)
                {
                    throw new Exception(string.Format("Error reading MPS profile {0}: {1}", FileName, Exc.Message));
                }
            }
        }

        /// <summary>
        /// Saves the user profiles
        /// </summary>
        public void SaveUserProfiles
            (
            )
        {
            foreach (MPSProfile Profile in Profiles)
            {
                if (Profile.UserProfile)
                {
                    string FileName = UserFolder + Path.DirectorySeparatorChar + Profile.Name + ".mps";
                    if (File.Exists(FileName))
                    {
                        File.Delete(FileName);
                    }

                    Profile.WriteToFile(FileName);
                }
            }
        }
    }
}
