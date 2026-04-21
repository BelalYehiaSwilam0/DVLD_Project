using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer_DVLD;
using Microsoft.Win32;

namespace DVLD_UserContext
{
    public static class clsGlobal
    {
        public static clsUsers CurrentUser;
       
      public static bool RememberUserNameAndPassword(string userName, string password)
        {
            string CurrentUserPath = @"SOFTWARE\Login";
            string valueName = "CurrentAccount";
            if (string.IsNullOrEmpty(userName))
            {
                try
                {
                    using(RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        using (RegistryKey key = baseKey.OpenSubKey(CurrentUserPath, true))
                        {
                            if (key != null)
                            {
                                key.DeleteValue(valueName);
                                return true;
                            }
                        }
                    }
                   

                }
                catch (Exception)
                {
                    return false;
                    //throw;
                }
            }

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\Login";
            string valueData = userName + "#//#" + password;
            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);

                //MessageBox.Show($"Value {valueName} successfully written to the Registry.", "Remember Me", MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred {ex.Message}");
                return false;
            }
        }

        
        public static bool GetStoredCredential(ref  string userName)
        {
            

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\Login";
            string valueName = "CurrentAccount";

            try
            {
                // Read the value from the Registry
                string value = Registry.GetValue(keyPath, valueName, null) as string;


                if (value != null)
                {
                    string[] result = value.Split(new string[] { "#//#" }, StringSplitOptions.None);
                                   userName = result[0];
                                   //password = result[1];
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred {ex.Message}");
                return false;
            }
        }
    }
}
