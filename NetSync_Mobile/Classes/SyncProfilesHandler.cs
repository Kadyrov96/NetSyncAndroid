using Android.App;

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace NetSync_Mobile
{
    /// <summary>
    /// Provides methods to manage synchronization profiles.
    /// </summary>
    class SyncProfilesHandler
    {
        static internal string syncProfilesStore = "/storage/emulated/0/profiles.dat";
        static public List<SyncProfile> AvailableProfilesList { get; set; }
        static public List<SyncProfile> SelectedProfilesList { get; set; }

        /// <summary>
        /// Creates new sync profile using entered name and selected folder.
        /// </summary>
        static public bool AddNewProfile(string name, string path, Activity currentActivity)
        {
            SyncProfile newProfile = new SyncProfile(name, path, DateTime.Now.ToString());
            if (CheckInputData(newProfile, currentActivity))
            {
                LoadProfiles();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Validates input name and folder to avoid the similar profiles.
        /// </summary>
        static bool CheckInputData(SyncProfile newProfile, Activity currentActivity)
        {
            if (newProfile.ProfileName == "" || newProfile.ProfileSyncFolderPath == "")
            {
                MessageDisplayer.ShowAlertMessage(currentActivity,
                    "Profile adding error", "Attempt to add profile with empty name or/and empty folderpath.");
                return false;
            }
            else
            {
                if (AvailableProfilesList.Count != 0)
                {
                    if(AvailableProfilesList.Any(profile => profile.ProfileName == newProfile.ProfileName))
                    {
                        MessageDisplayer.ShowAlertMessage(currentActivity,
                            "Profile adding error", "Profile with entered name is already exists. Please, enter other name");
                        return false;
                    }
                    else if (AvailableProfilesList.Any(profile => profile.ProfileSyncFolderPath == newProfile.ProfileSyncFolderPath))
                    {
                        MessageDisplayer.ShowAlertMessage(currentActivity,
                            "Profile adding error", "Profile with selected folder is already exists.Please, choose other folder");
                        return false;
                    }
                    else
                    {
                        SaveProfile(newProfile);
                        return true;
                    }
                }
                else
                {
                    SaveProfile(newProfile);
                    return true;
                }
            }     
        }

        /// <summary>
        /// Saves new created profile to local store.
        /// </summary>
        static void SaveProfile(SyncProfile profile)
        {
            string profileInfo = profile.ProfileName + "|" +
                    profile.ProfileSyncFolderPath + "|" + profile.SyncDateTime + Environment.NewLine;

            if (File.Exists(syncProfilesStore))
                File.AppendAllText(syncProfilesStore, profileInfo, Encoding.UTF8);
            else
                File.WriteAllText(syncProfilesStore, profileInfo, Encoding.UTF8);
        }

        /// <summary>
        /// Uploads profiles from local store.
        /// </summary>
        static public void LoadProfiles()
        {
            LoadEmptyProfiles();
            if (File.Exists(syncProfilesStore))
            {
                string[] syncProfilesArray = File.ReadAllLines(syncProfilesStore);
                foreach (var profile in syncProfilesArray)
                {
                    string[] substrings = profile.Split('|');
                    AvailableProfilesList.Add(new SyncProfile(substrings[0], substrings[1], substrings[2]));
                }
            }
        }

        /// <summary>
        /// Creates new list to hold profiles.
        /// </summary>
        static void LoadEmptyProfiles()
        {
            AvailableProfilesList = new List<SyncProfile>();
            SelectedProfilesList = new List<SyncProfile>();
        }

        /// <summary>
        /// Deletes profile with entered name and saves changes in local store.
        /// </summary>
        static public void DeleteProfile(string name)
        {
            for (int i = 0; i < AvailableProfilesList.Count; i++)
            {
                if (AvailableProfilesList[i].ProfileName == name)
                    AvailableProfilesList.RemoveAt(i);
            }

            File.Delete(syncProfilesStore);
            foreach (var item in AvailableProfilesList)
                SaveProfile(item);
        }

        /// <summary>
        /// Updates profile's date and time of the synchronization.
        /// </summary>
        static public void UpdateProfile(SyncProfile profile)
        {
            string[] syncProfilesArray = File.ReadAllLines(syncProfilesStore);
            foreach (var profileString in syncProfilesArray)
            {
                if (profileString.Contains(profile.ProfileName))
                {
                    profileString.Replace(profile.SyncDateTime, DateTime.Now.ToString());
                    break;
                }                 
            }

            SaveProfile(profile);
        }
    }
}
