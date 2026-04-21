using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsSettings
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int SettingID { set; get; }
        public string SettingName { set; get; }
        public short SettingValue { set; get; }

        public clsSettings()

        {
            this.SettingID = -1;
            this.SettingName = "";
            this.SettingValue = -1;

            Mode = enMode.AddNew;

        }

        private clsSettings(int ID, string SettingName,short settingValue)

        {
            this.SettingID = ID;
            this.SettingName = SettingName;
            this.SettingValue= settingValue;


            Mode = enMode.Update;

        }

        private bool _AddNewSetting()
        {
            //call DataAccess Layer 

            this.SettingID = clsSettingsData.AddNewSetting(this.SettingName,this.SettingValue);

            return (this.SettingID != -1);
        }

        private bool _UpdateSetting()
        {
            //call DataAccess Layer 

            return clsSettingsData.UpdateSetting(this.SettingID, this.SettingName,this.SettingValue);

        }

        public static clsSettings Find(int SettingID)
        {

            string SettingName = "";
            short SettingValue = -1;

            if (clsSettingsData.FindSettingInfoByID(SettingID, ref SettingName,ref SettingValue))

                return new clsSettings(SettingID, SettingName,SettingValue);
            else
                return null;

        }

        public static clsSettings Find(string SettingName)
        {

            int SettingID = -1;
            short SettingValue = -1;

            if (clsSettingsData.FindSettingInfoByName(SettingName, ref SettingID, ref SettingValue))

                return new clsSettings(SettingID, SettingName, SettingValue);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewSetting())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateSetting();

            }




            return false;
        }

        public static DataTable GetAllSettings()
        {
            return clsSettingsData.GetAllSettings();

        }

        public static bool DeleteCountry(int SettingID)
        {
            return clsSettingsData.DeleteSettingByID(SettingID);
        }


    }
}
