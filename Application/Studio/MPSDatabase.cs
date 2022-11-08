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

            Reload();
        }

        /// <summary>
        /// Enumerates the profiles in the database
        /// </summary>
        /// <returns>Enumeration of profiles</returns>
        public IEnumerable<MPSProfile> GetProfiles
            (
            )
        {
            foreach (MPSProfile Profile in Profiles)
            {
                yield return Profile;
            }
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
            GenerateFactoryAverageProfile();
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
                    Profile.FileName = FileName;

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
                    if (File.Exists(Profile.FileName))
                    {
                        continue;
                    }

                    Profile.FileName = UserFolder + Path.DirectorySeparatorChar + Profile.Name + ".mps";
                    Profile.WriteToFile(Profile.FileName);
                }
            }
        }

        /// <summary>
        /// Adds a new profile
        /// </summary>
        /// <param name="Profile">Profile to add</param>
        public void Add
            (
            MPSProfile Profile
            )
        {
            Profiles.Add(Profile);

            string Name = Guid.NewGuid().ToString();

            Profile.FileName = UserFolder + Path.DirectorySeparatorChar + Name + ".mps";
            Profile.WriteToFile(Profile.FileName);
        }

        /// <summary>
        /// Deletes a user profile
        /// </summary>
        /// <param name="Profile">Profile to delete</param>
        public void Remove
            (
            MPSProfile Profile
            )
        {
            if (Profile.UserProfile == true)
            {
                Profiles.Remove(Profile);
                File.Delete(Profile.FileName);
            }
        }

        /// <summary>
        /// Sets the name of a profile
        /// </summary>
        /// <param name="Profile">Profile to change</param>
        /// <param name="NewName">New name for profile</param>
        public void SetProfileName
            (
            MPSProfile Profile,
            string NewName
            )
        {
            if (Profile.UserProfile == true)
            {
                Profile.Name = NewName;
                Profile.WriteToFile(Profile.FileName);
            }
        }

        /// <summary>
        /// Generates a profile based on the average of the factory-calibrated MPSs
        /// </summary>
        private void GenerateFactoryAverageProfile
            (
            )
        {
            // fixme - to do
            // only generate if there is more than one factory calibrated profile in the database
        }
    }
}
