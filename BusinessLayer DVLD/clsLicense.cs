using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLayer_DVLD
{
    public class clsLicense
    {
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enum enIssueReason { FirstTime = 1 , Renew = 2 , DamagedReplacement = 3 , LostReplacement = 4 };
        public enIssueReason IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        public clsDetainedLicenses DetainedLicenseInfo;
        public bool IsDetained
        {
            get { return clsDetainedLicenses.IsLicenseDetainedByID(this.LicenseID); }
        }
        enum enMode { AddNew = 0 , Update = 1 }
        enMode Mode = enMode.Update;

        public string IssueReasonText
        {
            get {return GetIssueReasonTest(this.IssueReason); }
        }
        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = 0;
            DriverID = 0;
            LicenseClassID = 0;
            IssueDate = DateTime.MinValue;
            ExpirationDate = DateTime.MinValue;
            Notes = string.Empty;
            PaidFees = 0;
            IsActive = false;
            IssueReason = 0;
            CreatedByUserID = 0;
            Mode = enMode.AddNew;
        }
        private clsLicense(int licenseID, int applicationID, int driverID, int licenseClassID, DateTime issueDate, DateTime expirationDate, string notes, decimal paidFees, bool isActive, enIssueReason issueReason, int createdByUserID)
        {
            LicenseID = licenseID;
            DetainedLicenseInfo = clsDetainedLicenses.GetDetainedLicenseByLicenseID(this.LicenseID);
            ApplicationID = applicationID;
            DriverID = driverID;
            DriverInfo = clsDriver.FindDriverInfoByID(driverID);
            LicenseClassID = licenseClassID;
            LicenseClassInfo = clsLicenseClass.GetLicenseClassByID(LicenseClassID);
            IssueDate = issueDate;
            ExpirationDate = expirationDate;
            Notes = notes;
            PaidFees = paidFees;
            IsActive = isActive;
            IssueReason = issueReason;
            CreatedByUserID = createdByUserID;
            Mode = enMode.Update;
        }
        public static clsLicense FindLicenseByID(int licenseID)
        {
            int applicationID = -1, driverID = -1, licenseClassID = -1, createdByUserID = -1;
            DateTime issueDate = DateTime.MinValue, expirationDate = DateTime.MinValue;
            string notes = string.Empty;
            decimal paidFees = -1;
            bool isActive = false;
            byte issueReason = 1;

            if (clsLicenseData.FindLicenseByID(licenseID, ref applicationID, ref driverID, ref licenseClassID
                , ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref createdByUserID))
            {
                return new clsLicense(licenseID, applicationID, driverID, licenseClassID
                , issueDate, expirationDate, notes, paidFees, isActive, (enIssueReason)issueReason, createdByUserID);
            }
            else
                return null;

        }
        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID,this.DriverID,this.LicenseClassID, this.IssueDate,this.ExpirationDate
                ,this.Notes,this.PaidFees,this.IsActive,(byte)this.IssueReason,this.CreatedByUserID);
            return (this.LicenseID != -1);
        }
        public static DataTable GetAllLicensesByDriverID(int DriverID)
        {
            return clsLicenseData.GetAllLicensesByDriverID(DriverID);
        }
        public static bool IsLicenseExistByLicenseID(int licenseID)
        {
            return clsLicenseData.IsLicenseExistByID(licenseID);
        }
        private bool _UpdateLicenseByID()
        {
            return clsLicenseData.UpdateLicenseByID( this.LicenseID,this.ApplicationID,this.DriverID,this.LicenseClassID,this.IssueDate,
                this.ExpirationDate,this.Notes,this.PaidFees,this.IsActive,(byte)this.IssueReason,this.CreatedByUserID
            );
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {

            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);

        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    if (_UpdateLicenseByID())
                    {
                        return true;
                    }
                    else
                        return false;
            }
            return true;
        }

        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public int Detain(decimal FineFees , int CreatedByUserID)
        {
            clsDetainedLicenses detainedLicenses = new clsDetainedLicenses();
            detainedLicenses.LicenseID = this.LicenseID;
            detainedLicenses.DetainDate = DateTime.Now;
            detainedLicenses.FineFees = FineFees;
            detainedLicenses.CreatedByUserID = CreatedByUserID;

            if(!detainedLicenses.Save())
                return -1;
            return detainedLicenses.DetainID;
        }
        public bool DeactivateCurrentLicense()
        {
            return (clsLicenseData.DeactivateLicense(this.LicenseID));
        }

        public static string GetIssueReasonTest(enIssueReason issueReason)
        {
            switch(issueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "First Time";
            }
        }

        public clsLicense RenewLicense(string notes,int createdByUserID)
        {
            // First we create a Application

            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.GetApplicationTypeInfoByID((int)clsApplication.enApplicationType.RenewDrivingLicense).ApplicationFees;
            Application.CreatedByUserID = createdByUserID;

            if(!Application.Save())
            {
                return null;
            }
            
            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = notes;
            NewLicense.PaidFees = this.LicenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = createdByUserID;

            if(!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();
            return NewLicense;
        }

        public clsLicense Replace(enIssueReason issueReason , int createdByUserID)
        {
            // First we create a Application

            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (issueReason == enIssueReason.DamagedReplacement)?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense:
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.GetApplicationTypeInfoByID(Application.ApplicationTypeID).ApplicationFees;
            Application.CreatedByUserID = createdByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClassID = this.LicenseClassID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;// no fees for the license because it's a replacement.;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = createdByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();
            return NewLicense;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationTypes.GetApplicationTypeInfoByID((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
            ApplicationID = Application.ApplicationID;

            return this.DetainedLicenseInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        }
    }
}
