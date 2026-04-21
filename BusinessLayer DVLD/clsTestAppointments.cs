using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsTestAppointments
    {
        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }

        public int RetakeTestApplicationID {  get; set; }
        public clsApplication RetakeTestApplicationInfo { get; set; }
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.Update;

        public int TestID
        {
            get { return _GetTestID(); }

        }
        public clsTestAppointments()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = 0;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;
        }

       private clsTestAppointments(int testAppointmentID, clsTestType.enTestType testTypeID,int localDrivingLicenseApplicationID , DateTime appointmentDate,
            decimal paidFees, int createdByUserID , bool isLocked, int retakeTestApplicationID)
        {
            this.TestAppointmentID= testAppointmentID;
            this.TestTypeID=testTypeID;
            this.LocalDrivingLicenseApplicationID=localDrivingLicenseApplicationID;
            this.AppointmentDate = appointmentDate;
            this.PaidFees=paidFees;
            this.CreatedByUserID=createdByUserID;
            this.IsLocked=isLocked;
            this.RetakeTestApplicationID=retakeTestApplicationID;
            this.RetakeTestApplicationInfo = clsApplication.FindApplicationByID(this.RetakeTestApplicationID);
            Mode = enMode.Update;
            
        }
        public static clsTestAppointments FindTestAppointmentByID(int testAppointmentID)
        {
            int testTypeID = 0, localDrivingLicenseApplicationID = 0, createdByUserID = 0;
            DateTime appointmentDate = DateTime.Now;
            bool isLocked = false;
            decimal paidFees = 0;
            int RetakeTestApplicationID = -1;

            if (clsTestAppointmentsData.FindTestAppointmentByID(testAppointmentID, ref testTypeID, ref localDrivingLicenseApplicationID,
                ref appointmentDate,  ref paidFees, ref createdByUserID, ref isLocked,ref RetakeTestApplicationID))
            {
                return new clsTestAppointments(testAppointmentID,(clsTestType.enTestType)testTypeID,localDrivingLicenseApplicationID,appointmentDate,paidFees,createdByUserID,isLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }
        public static clsTestAppointments FindTestAppointmentByTestTypeIDAndLocalDrivingLisID(int testTypeID, int localDrivingLicenseApplicationID)
        {
            int testAppointmentID = 0, createdByUserID = 0 , RetakeTestApplicationID = -1;
            bool isLocked = false;
            decimal paidFees = 0;
            DateTime appointmentDate = DateTime.Now;


            if (clsTestAppointmentsData.FindTestAppointmentByTestTypeIDAndLocalDrivingLisID(ref testAppointmentID, testTypeID, ref localDrivingLicenseApplicationID,
                appointmentDate, ref paidFees, ref createdByUserID ,ref isLocked,ref RetakeTestApplicationID))
            {
                return new clsTestAppointments(testAppointmentID, (clsTestType.enTestType)testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }
        public static clsTestAppointments FindLastRowInTestAppointments()
        {
            int testAppointmentID = 0;
            int testTypeID = 0, localDrivingLicenseApplicationID = 0, createdByUserID = 0, RetakeTestApplicationID = -1;
            DateTime appointmentDate = DateTime.Now;
            bool isLocked = false;
            decimal paidFees = 0;

            if (clsTestAppointmentsData.FindLastRowInTestAppointments(ref testAppointmentID, ref testTypeID, ref localDrivingLicenseApplicationID,
                ref appointmentDate, ref paidFees, ref createdByUserID, ref isLocked,ref RetakeTestApplicationID))
            {
                return new clsTestAppointments(testAppointmentID, (clsTestType.enTestType)testTypeID, localDrivingLicenseApplicationID, appointmentDate, paidFees, createdByUserID, isLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }
        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsData.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID,this.IsLocked,this.RetakeTestApplicationID);
            return (TestAppointmentID != -1);
        }
        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsData.UpdateTestAppointment(this.TestAppointmentID,(int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate,
                this.PaidFees, this.CreatedByUserID, this.IsLocked,this.RetakeTestApplicationID);
        }
        public static bool DeleteApplicationIDByID(int testAppointmentID)
        {
            return clsTestAppointmentsData.DeleteTestAppointmentByID(testAppointmentID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    if (_UpdateTestAppointment())
                    {
                        return true;
                    }
                    else
                        return false;
            }
            return true;
        }
        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentsData.GetAllTestAppointments();
        }
        public static DataTable GetTestAppointmentsByLocalDrivingLicenseAppIDAndTestTypeID(int localDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentsData.GetTestAppointmentsByLocalDrivingLicenseAppIDAndTestTypeID(localDrivingLicenseApplicationID,(byte)TestTypeID);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentsData.GetTestID(TestAppointmentID);
        }
    }
}
