using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsDetainedLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsers CreatedByUserInfo;
        public bool IsReleased { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleasedByUserID { get; set; }
        public clsUsers ReleasedByUserInfo;
        public int? ReleaseApplicationID { get; set; }

       

        public clsDetainedLicenses()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Now;
            FineFees = 0;
            CreatedByUserID = -1;
            IsReleased = false;
            ReleaseDate = DateTime.MaxValue;
            ReleasedByUserID = 0;
            ReleaseApplicationID = -1;
            Mode = enMode.AddNew;
        }
        private clsDetainedLicenses(int detainID, int licenseID, DateTime detainDate, decimal fineFees, int createdByUserID,
                            bool isReleased, DateTime? releaseDate, int? releasedByUserID, int? releaseApplicationID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            CreatedByUserInfo = clsUsers.FindUserByID(CreatedByUserID);
            IsReleased = isReleased;
            ReleaseDate = releaseDate;
            ReleasedByUserID = releasedByUserID;
            if(releasedByUserID != null)
            {
                ReleasedByUserInfo = clsUsers.FindUserByPersonID((int)this.ReleasedByUserID);
            }
            
            ReleaseApplicationID = releaseApplicationID;
            Mode = enMode.Update;
        }
        public static clsDetainedLicenses GetByID(int detainID)
        {
            int licenseID = -1, createdByUserID = -1;
            int? releasedByUserID = -1, releaseApplicationID = -1;
            DateTime detainDate = DateTime.MinValue;
            DateTime? releaseDate = DateTime.MaxValue;
            decimal fineFees = 0;
            bool isReleased = false;

            if (clsDetainedLicensesData.GetDetainedLicenseInfoByID(detainID, ref licenseID, ref detainDate, ref fineFees, ref createdByUserID,
                                                  ref isReleased, ref releaseDate, ref releasedByUserID, ref releaseApplicationID))
            {
                return new clsDetainedLicenses(detainID, licenseID, detainDate, fineFees, createdByUserID,
                                               isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }

        public static clsDetainedLicenses GetDetainedLicenseByLicenseID(int LicenseID)
        {
            int detainID = -1, createdByUserID = 0;
            int? releasedByUserID = null, releaseApplicationID = null;
            DateTime detainDate = DateTime.MinValue;
            DateTime? releaseDate = DateTime.MaxValue;
            decimal fineFees = 0;
            bool isReleased = false;

            if (clsDetainedLicensesData.GetDetainedLicenseByLicenseID(ref detainID, LicenseID, ref detainDate, ref fineFees, ref createdByUserID,
                                                  ref isReleased, ref releaseDate, ref releasedByUserID, ref releaseApplicationID))
            {
                return new clsDetainedLicenses(detainID, LicenseID, detainDate, fineFees, createdByUserID,
                                               isReleased, releaseDate, releasedByUserID, releaseApplicationID);
            }
            else
            {
                return null;
            }
        }
        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return (this.DetainID != -1);
        }
        public static bool IsLicenseDetainedByID(int licenseID)
        {
            return clsDetainedLicensesData.IsLicenseDetainedByID(licenseID);
        }
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicensesData.UpdateByID(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainedLicense();

            }

            return false;
        }
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesData.GetAllDetainedLicenses();
        }

        public bool ReleaseDetainedLicense(int RelasedReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicensesData.ReleaseDetainedLicense(this.DetainID,RelasedReleasedByUserID,ref ReleaseApplicationID);
        }
    }
}

