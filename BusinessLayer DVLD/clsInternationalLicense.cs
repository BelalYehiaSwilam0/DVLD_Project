using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsInternationalLicense :clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public short DefaultValidityLength
        {
            get
            {
                return clsSettings.Find(1).SettingValue;
            }
        }

        public clsInternationalLicense()
        {
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;

            Mode = enMode.AddNew;
        }

        private clsInternationalLicense(int applicationID, int applicantPersonID, DateTime applicationDate, 
            enApplicationStatus applicationStatus, DateTime lastStatusDate, decimal paidFees,int internationalLicenseID,
            int driverID, int issuedUsingLocalLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive,
            int createdByUserID)
        {
            base.ApplicationID = applicationID;
            base.ApplicantPersonID = applicantPersonID;
            base.ApplicationDate = applicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = applicationStatus;
            base.LastStatusDate = lastStatusDate;
            base.PaidFees = paidFees;

            this.InternationalLicenseID = internationalLicenseID;
            this.DriverID = driverID;
            this.DriverInfo = clsDriver.FindDriverInfoByID(this.DriverID);
            this.IssuedUsingLocalLicenseID = issuedUsingLocalLicenseID;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;
            this.IsActive = isActive;
            this.CreatedByUserID = createdByUserID;

            Mode = enMode.Update;
        }

        public static clsInternationalLicense FindInternationalLicenseByID(int internationalLicenseID)
        {
            int applicationID = -1;
            int driverID = -1;
            int issuedUsingLocalLicenseID = -1;
            DateTime issueDate = DateTime.Now;
            DateTime expirationDate = DateTime.Now;
            bool isActive = false;
            int createdByUserID = 1;

            if (clsInternationalLicenseData.FindInternationalLicenseByID(internationalLicenseID, ref applicationID, ref driverID, ref issuedUsingLocalLicenseID, ref issueDate, ref expirationDate, ref isActive, ref createdByUserID))
            {
                clsApplication Application = clsApplication.FindApplicationByID(applicationID);
                if (Application == null)
                    return null;

                return new clsInternationalLicense(Application.ApplicationID,Application.ApplicantPersonID,
                    Application.ApplicationDate, Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, internationalLicenseID, driverID, issuedUsingLocalLicenseID, issueDate, expirationDate, isActive, createdByUserID);
            }
            else
                return null;
        }
        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicenseData._AddNewInternationalLicense(
                this.ApplicationID,
                this.DriverID,
                this.IssuedUsingLocalLicenseID,
                this.IssueDate,
                this.ExpirationDate,
                this.IsActive,
                this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicenseData.UpdateInternationalLicense(
                this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);
        }

        public static bool DeleteInternationalLicenseByID(int internationalLicenseID)
        {
            return clsInternationalLicenseData.DeleteInternationalLicenseByID(internationalLicenseID);
        }

        public static DataTable InternationalLicenses(int driverID)
        {
            return clsInternationalLicenseData.InternationalLicenses(driverID);
        }
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();
        }

        public static bool IsInternationalLicenseActive(int DriverID)
        {
            return clsInternationalLicenseData.IsInternationalLicenseActive(DriverID);
        }
        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if(!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateInternationalLicense();

            }
            return false;
        }
    }
}

