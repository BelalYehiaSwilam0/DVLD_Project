using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsLocalDrivingLicenseApplication :clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClassInfo;

        public string PersonFullName
        {
            get { return base.ApplicantFullName; }    
        }
        public clsLocalDrivingLicenseApplication ()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = 0;

            Mode = enMode.AddNew ;
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplication, int licenseClassID, 
            int applicationID, int applicantPersonID, DateTime applicationDate, int applicationTypeID, 
            enApplicationStatus applicationStatus, DateTime lastStatusDate, decimal paidFees, int createdByUserID)
        {
            this.LocalDrivingLicenseApplicationID= localDrivingLicenseApplication;
            this.LicenseClassID= licenseClassID;
            this.LicenseClassInfo = clsLicenseClass.GetLicenseClassByID(licenseClassID);
            this.ApplicationID = applicationID;
            this.ApplicantPersonID= applicantPersonID;
            this.ApplicationDate= applicationDate;
            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserID = createdByUserID;

            Mode = enMode.Update;
        }
        public static clsLocalDrivingLicenseApplication FindLocalDrivingLicenseApplicationByID(int localDrivingLicenseApplication)
        {
            int applicationID = -1 , licenseClassID = -1 ;

            bool IsFound = clsLocalDrivingLicenseApplicationsData.FindLocalDrivingLicenseApplicationByID(localDrivingLicenseApplication,ref applicationID,ref licenseClassID);

            if(IsFound)
            {
               clsApplication Application = clsApplication.FindApplicationByID(applicationID);
                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplication, licenseClassID,applicationID,Application.ApplicantPersonID
                    ,Application.ApplicationDate,Application.ApplicationTypeID,Application.ApplicationStatus,Application.LastStatusDate
                    ,Application.PaidFees,Application.CreatedByUserID);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int localDrivingLicenseApplication = -1, licenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID , ref localDrivingLicenseApplication, ref licenseClassID);

            if (IsFound)
            {
                clsApplication Application = clsApplication.FindApplicationByID(ApplicationID);
                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplication, licenseClassID, ApplicationID, Application.ApplicantPersonID
                    , Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate
                    , Application.PaidFees, Application.CreatedByUserID);
            }
            else
                return null;
        }
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            //call DataAccess Layer 

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData._AddNewLocalDrivingLicenseApplication(this.ApplicationID,this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }
        public static bool DeleteLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplicationByID(LocalDrivingLicenseApplicationID);
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            //call DataAccess Layer 

            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication
                (
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);

        }
        public bool Save()
        {
            //Because of inheritance first we call the save method in the base class.
            //it will take care of adding all information to the application table.

            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;
               
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLocalDrivingLicenseApplication();

            }
            return false;

        }
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalDrivingLicenseApplications();
        }

        public bool Delete()
        {
            bool IsLocalDrivingLicenseApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            //First we delete the Local Driving License Application
            IsLocalDrivingLicenseApplicationDeleted = clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplicationByID(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingLicenseApplicationDeleted)
                return false;
            //Then we delete the base Application
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;
            
        }

        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        public static  bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsData.DoesAttendTestType(LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public byte TotalTrialsPerTest(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            return clsLocalDrivingLicenseApplicationsData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestType);
        }

        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestType)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID,this.LicenseClassID,TestType);
        }

        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirstTime(string Note,int CreatedByUserID)
        {
            int DriverID = -1;
            clsDriver Driver = clsDriver.FindDriverInfoByPersonID(this.ApplicantPersonID);

            if(Driver ==null)
            {
                // we check if the driver already there for the person

                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                    return -1;
            }
            else
            {
                DriverID = Driver.DriverID;
            }

            //now we have a driver , so we add new licnese 

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Note;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;
            }
            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID,this.LicenseClassID);
        }
       
    }
}
