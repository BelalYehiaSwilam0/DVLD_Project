using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer_DVLD
{
    public class clsUsers
    {
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public enum enMode { AddNew = 0, Update = 1 }
        enMode Mode = enMode.Update;

        public clsUsers()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;

            Mode = enMode.AddNew;
        }

        private clsUsers(int userID, int personID, string userName, string password, bool isActive)
        {
            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = userName;
            this.Password = password;
            this.IsActive = isActive;

            Mode = enMode.Update;
        }
        public static clsUsers FindUserByID(int UserID)
        {
            int PersonID = 0;
            string UserName = string.Empty, Password = string.Empty;
            bool IsActive = false;

            if (clsDataUsers.FindUserByID(ref UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {

                return new clsUsers(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;
        }
        public static clsUsers FindUserByPersonID(int personID)
        {
            int userID = 0;
            string UserName = string.Empty, Password = string.Empty;
            bool IsActive = false;

            if (clsDataUsers.FindUserByPersonID(ref userID, ref personID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUsers(userID, personID, UserName, Password, IsActive);
            }
            else
                return null;
        }

        public static clsUsers FindUserByUsernameAndPassword(string userName, string password)
        {
            int personID = 0;
            int userID = 0;
            bool isActive = false;
            if(clsDataUsers.FindUserByUsernameAndPassword(ref userID,ref personID, ref userName,ref password,ref isActive))
            {
                return new clsUsers(userID, personID, userName, password, isActive);
            }
            else
                return null;
        }
        private bool _AddNewPerson()
        {
            this.UserID = clsDataUsers.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (UserID != -1);
        }
        private bool _UpdatePersonInfoByID()
        {
            return clsDataUsers.UpdateUserByID(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }
        public static bool DeletePersonByID(int PersonID)
        {
            return clsDataUsers.DeleteUserByID(PersonID);
        }
        public static DataTable GetAllUsers()
        {
            return clsDataUsers.GetAllUsers();
        }
        public static bool IsUserExist(int PersonID)
        {
            return clsDataUsers.IsUserExist(PersonID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsDataUsers.IsUserExist(UserName);
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

