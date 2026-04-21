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
    public class clsDriver
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }

        public clsPerson PersonInfo; 
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        enum enMode { AddNew = 0 , Update = 1 }
        enMode Mode = enMode.Update;
        public clsDriver()
        {
            DriverID = -1;
            PersonID = 0;
            CreatedByUserID = 0;
            CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate)
        {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;
            this.Mode = enMode.Update;
        }
        public static clsDriver FindDriverInfoByPersonID( int PersonID)
        {
            int Driver = 0 , CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.MinValue;

            if (clsDriverData.FindDriverInfoByPersonID(ref Driver, PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(Driver, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }

        public static clsDriver FindDriverInfoByID(int Driver)
        {
            int PersonID = 0, CreatedByUserID = 0;
            DateTime CreatedDate = DateTime.MinValue;

            if (clsDriverData.FindDriverInfoByID( Driver, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(Driver, PersonID, CreatedByUserID, CreatedDate);
            }
            else
                return null;
        }
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return this.DriverID != -1;
        }

        private bool _UpdateDriver()
        {
            //call DataAccess Layer 

            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDriver();

            }

            return false;
        }


    }
}
