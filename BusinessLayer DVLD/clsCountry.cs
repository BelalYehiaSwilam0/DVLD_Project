using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessLayer_DVLD
{
    public class clsCountry
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int CountryID { set; get; }
        public string CountryName { set; get; }
       

        public clsCountry()

        {
            this.CountryID = -1;
            this.CountryName = "";

            Mode = enMode.AddNew;

        }

        private clsCountry(int ID, string CountryName)

        {
            this.CountryID = ID;
            this.CountryName = CountryName;
           

            Mode = enMode.Update;

        }

        private bool _AddNewCountry()
        {
            //call DataAccess Layer 

            this.CountryID = clsCountryData.AddNewCountry(this.CountryName);

            return (this.CountryID != -1);
        }

        private bool _UpdateContact()
        {
            //call DataAccess Layer 

            return clsCountryData.UpdateCountry(this.CountryID, this.CountryName);

        }

        public static clsCountry Find(int ID)
        {

            string CountryName = "";
          

            int CountryID = -1;

            if (clsCountryData.GetCountryInfoByCountryID(ID, ref CountryName))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }

        public static clsCountry Find(string CountryName)
        {

            int ID = -1;

            if (clsCountryData.GetCountryInfoByName(CountryName, ref ID))

                return new clsCountry(ID, CountryName);
            else
                return null;

        }


        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateContact();

            }




            return false;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();

        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountryData.DeleteCountry(ID);
        }

        public static bool isCountryExist(int ID)
        {
            return clsCountryData.IsCountryExist(ID);
        }

        public static bool isCountryExist(string CountryName)
        {
            return clsCountryData.IsCountryExist(CountryName);
        }



    }
}
