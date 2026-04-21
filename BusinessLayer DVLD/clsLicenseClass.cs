using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsLicenseClass
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { get; set; }
       public string ClassName { get; set; }
       public string ClassDescription { get; set; }
       public byte MinimumAllowedAge { get; set; }
       public byte DefaultValidityLength { get; set; }
       public decimal ClassFees { get; set; }
        public clsLicenseClass()

        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

            Mode = enMode.AddNew;

        }
        private clsLicenseClass(int licenseClassID, string className, string classDescription, byte minimumAllowedAge, byte defaultValidityLength, decimal classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
            Mode = enMode.Update;
        }
        public static DataTable GetAllLicenseClass()
        {
            return clsLicenseClassesData.GetAllLicenseClass();
        }
        public static clsLicenseClass GetLicenseClassByID(int licenseClassID)
        {
            string className = string.Empty; string classDescription = string.Empty;
            byte minimumAllowedAge = 0; byte defaultValidityLength = 0;
              decimal classFees = 0;

            if(clsLicenseClassesData.GetLicenseClassByID(licenseClassID,ref className,ref  classDescription,ref minimumAllowedAge,ref defaultValidityLength,ref classFees))
            {
                return new clsLicenseClass(licenseClassID, className, classDescription, minimumAllowedAge, defaultValidityLength, classFees);
            }

            return null;
        }
        public static clsLicenseClass GetLocalDrivingLicenseInfoByName(string className )
        {
            int licenseClassID = 0; string classDescription = string.Empty;
            byte minimumAllowedAge = 0; byte defaultValidityLength = 0;
            decimal classFees = 0;

            if (clsLicenseClassesData.GetLocalDrivingLicenseInfoByName(className, ref licenseClassID, ref classDescription, ref minimumAllowedAge, ref defaultValidityLength, ref classFees))
            {
                return new clsLicenseClass(licenseClassID, className, classDescription, minimumAllowedAge, defaultValidityLength, classFees);
            }

            return null;
        }
      
        private bool _AddNewLicenseClass()
        {
            //call DataAccess Layer 

            this.LicenseClassID = clsLicenseClassesData.AddNewLicenseClass(this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);


            return (this.LicenseClassID != -1);
        }

        private bool _UpdateLicenseClass()
        {
            //call DataAccess Layer 

            return clsLicenseClassesData.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription,
                this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicenseClass();

            }

            return false;
        }
    }
}
