using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsApplicationTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = 0;
            this.ApplicationTypeTitle = string.Empty;
            this.ApplicationFees = 0;
            Mode = enMode.AddNew;
        }

        public clsApplicationTypes(int applicationTypeID, string applicationTypeTitle, decimal applicationFees)
        {
            ApplicationTypeID = applicationTypeID;
            ApplicationTypeTitle = applicationTypeTitle;
            ApplicationFees = applicationFees;
            Mode = enMode.Update;
        }

        public static clsApplicationTypes GetApplicationTypeInfoByID(int applicationTypeID)
        {
            string applicationTypeTitle = string.Empty;
            decimal applicationFees = 0;
            if (clsApplicationTypesData.GetApplicationTypeInfoByID(applicationTypeID, ref applicationTypeTitle, ref applicationFees))
            {
                return new clsApplicationTypes(applicationTypeID, applicationTypeTitle, applicationFees);
            }
            return null;
        }

        public static clsApplicationTypes GetApplicationTypeInfoByName(string applicationTypeTitle)
        {
            int applicationTypeID = 0;
            decimal applicationFees = 0;
            if (clsApplicationTypesData.GetApplicationTypeInfoByName(applicationTypeTitle, ref applicationTypeID, ref applicationFees))
            {
                return new clsApplicationTypes(applicationTypeID, applicationTypeTitle, applicationFees);
            }
            return null;
        }

        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer 

            this.ApplicationTypeID = clsApplicationTypesData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationFees);


            return (this.ApplicationTypeID != -1);
        }
        private bool UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public static DataTable GetAllApplicatoinTypes()
        {
            return clsApplicationTypesData.GetAllApplicatoinTypes();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return UpdateApplicationType();
            }
            return false;
        }
    }
}
