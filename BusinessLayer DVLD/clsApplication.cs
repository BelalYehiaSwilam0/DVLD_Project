using System;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsApplication
    {

       public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 8
        };
       public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
 
       public int ApplicationID {  get; set; }
       public int ApplicantPersonID { get; set; }

        public clsPerson PersonInfo { get; set; }
       public string ApplicantFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
            
        }
       public DateTime ApplicationDate { get; set; }
       public int ApplicationTypeID { get; set; }
       public clsApplicationTypes ApplicationTypeInfo;
       public enApplicationStatus ApplicationStatus { set; get; }
       public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
       public DateTime LastStatusDate { get; set; }
       public decimal PaidFees { get; set; }
       public int CreatedByUserID { get; set; }
       public clsUsers CreatedByUserInfo;  

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.Update;
        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = 0;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = 0;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
           Mode = enMode.AddNew;
        }
        private clsApplication(int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, enApplicationStatus applicationStatus, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            this.ApplicationID = applicationID;
            this.ApplicantPersonID = applicantPersonID;
            this.PersonInfo = clsPerson.Find(applicantPersonID);
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypes.GetApplicationTypeInfoByID(applicationTypeID);
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUsers.FindUserByID(CreatedByUserID);
            Mode = enMode.Update;
        }

        public static clsApplication FindApplicationByID(int applicationID)
        {
            int applicantPersonID = 0, applicationTypeID = 0, createdByUserID = 0;
            DateTime applicationDate = DateTime.Now, lastStatusDate = DateTime.Now;
            byte applicationStatus = 1;
            decimal paidFees = 0;

            if (clsApplicationsData.GetApplicationInfoByID(applicationID, ref applicantPersonID, ref applicationDate,
                ref applicationTypeID, ref applicationStatus, ref lastStatusDate, ref paidFees, ref createdByUserID))
            {
                return new clsApplication(applicationID, applicantPersonID, applicationDate,
                    applicationTypeID, (enApplicationStatus)applicationStatus, lastStatusDate, paidFees, createdByUserID);
            }
            else
                return null;
        }

        public bool Cancel()
        {
            return clsApplicationsData.UpdateStatus(this.ApplicationID, (short)enApplicationStatus.Cancelled);
        }

        public bool SetComplete()
        {
            return clsApplicationsData.UpdateStatus(this.ApplicationID, (short)enApplicationStatus.Completed);
        }
       
        private bool _AddNewApplication()
         {
             this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicantPersonID,this.ApplicationDate,this.ApplicationTypeID,
                 (byte)this.ApplicationStatus, this.LastStatusDate,this.PaidFees,this.CreatedByUserID);
             return (ApplicationID != -1);
         }

        private bool _UpdateApplicationInfoByID()
        {
            return clsApplicationsData.UpdateApplication(this.ApplicationID,this.ApplicantPersonID,this.ApplicationDate,this.ApplicationTypeID,
                (byte)this.ApplicationStatus,this.LastStatusDate,this.PaidFees,this.CreatedByUserID );
        }

        public static bool IsApplicationActivating(string NationalNo, string className)
        {
            return clsApplicationsData.IsApplicationActivating(NationalNo, className);
        }

        public bool Delete()
        {
            return clsApplicationsData.DeleteApplicationIDByID(this.ApplicationID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    if (_UpdateApplicationInfoByID())
                    {
                        return true;
                    }
                    else
                        return false;
            }
            return true;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationsData.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID , int ApplicationTypeID)
        {
            return clsApplicationsData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID,clsApplication.enApplicationType applicationTypeID)
        {
            return clsApplicationsData.GetActiveApplicationID(PersonID, (int)applicationTypeID);
        }

        public static int GetActiveApplicationForLicenseClass(int PersonID,clsApplication.enApplicationType applicationTypeID , int LicenseClassID)
        {
            return clsApplicationsData.GetActiveApplicationIDForLicenseClass(PersonID, (int)applicationTypeID , LicenseClassID);
        }
    }
}

