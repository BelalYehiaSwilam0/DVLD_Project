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
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.Update;

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string FullName
        {
            get { return $"{FirstName} {SecondName} {ThirdName} {LastName}"; }
        }
        public byte Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public clsCountry CountryInfo;
        public string ImagePath { get; set; }
        
        public clsPerson()
        {
            this.PersonID = -1;
            this.NationalNo = string.Empty;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.DateOfBirth = default;
            this.Gender = 0;
            this.Address = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.NationalityCountryID = 0;
            this.ImagePath = string.Empty;

            Mode = enMode.AddNew;
        }
        private clsPerson(int personID, string nationalNo, string firstName, string secondName, string thirdName,
                 string lastName, DateTime dateOfBirth, byte gender, string address, string phone,
                 string email, int nationalityCountryID, string imagePath)
        {
            this.PersonID = personID;
            this.NationalNo = nationalNo ?? string.Empty;
            this.FirstName = firstName ?? string.Empty;
            this.SecondName = secondName ?? string.Empty;
            this.ThirdName = thirdName ?? string.Empty;
            this.LastName = lastName ?? string.Empty;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address ?? string.Empty;
            this.Phone = phone ?? string.Empty;
            this.Email = email ?? string.Empty;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath ?? string.Empty;
            this.CountryInfo = clsCountry.Find(nationalityCountryID);

            Mode = enMode.Update;
        }
        public static clsPerson Find(int PersonID)
        {
            string NationalNo = string.Empty, FirstName = string.Empty, SecondName = string.Empty, ThirdName = string.Empty,
                 LastName = string.Empty, Address = string.Empty, Phone = string.Empty, Email = string.Empty, ImagePath = string.Empty;
                DateTime DateOfBirth = DateTime.Now; byte Gender = 0; int NationalityCountryID = 0;
            bool IsFound = clsDataAccessPeople.FindPersonByID(ref PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
          ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);
            if (IsFound)
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                     DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }
        public static clsPerson Find(string NationalNo )
        {
            int PersonID = -1;
            string FirstName = string.Empty, SecondName = string.Empty, ThirdName = string.Empty,
                 LastName = string.Empty, Address = string.Empty, Phone = string.Empty, Email = string.Empty, ImagePath = string.Empty;
            DateTime DateOfBirth = DateTime.Now; byte Gender = 0; int NationalityCountryID = 0;
            bool IsFound = clsDataAccessPeople.FindPersonByNationalNo(ref PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
          ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);
            if (IsFound)
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                     DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }
        public static bool IsPersonExist(int PersonID)
        {
            return clsDataAccessPeople.IsPersonExistByID(PersonID);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsDataAccessPeople.IsPersonExistByNationalNo(NationalNo);
        }
        private bool _AddNewPerson()
        {
            this.PersonID=clsDataAccessPeople.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                        this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone,
                        this.Email, this.NationalityCountryID, this.ImagePath);
            return (PersonID != -1);
        }
        private bool _UpdatePersonInfoByID()
        {
            return (clsDataAccessPeople.UpdatePersonInfoByID(this.PersonID, this.NationalNo, this.FirstName,this.SecondName,this.ThirdName,this.LastName
                ,this.DateOfBirth,this.Gender,this.Address,this.Phone,this.Email,this.NationalityCountryID,this.ImagePath));
        }
        public static bool DeletePersonByID(int PersonID)
        {
            return clsDataAccessPeople.DeletePersonByID(PersonID);
        }
        public static DataTable GetAllPeople()
        {
            return clsDataAccessPeople.GetAllPeople();
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                    
                case enMode.Update:
                    if (_UpdatePersonInfoByID())
                    {
                        return true;
                    }
                    else
                        return false;
            }
            return true;
        }
    }
}
