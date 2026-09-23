using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public  class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }

        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }

        public int PersonID { set; get; }

        clsPerson PersonInfo;
        public string UserName { set; get; }

        public string Password { set; get; }

        public bool isActive { set; get; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            isActive = true;
        }

        private clsUser(int UserID,int PersonID,string UserName,string Password,bool isActive)
        {

            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.isActive = isActive;
            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.isActive);
            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.isActive);

        }

        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool isActive = true;
            if(clsUserData.GetUserInfoByUserID(UserID,ref PersonID,ref UserName,ref Password,ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "";
            string Password = "";
            bool isActive = true;
            if (clsUserData.GetUserInfoByPersonID(PersonID,ref UserID,ref UserName,ref Password,ref isActive))
            {
                return new clsUser(UserID,PersonID,UserName,Password,isActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser FindByUserNameAndPassword(string UserName,string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool isActive = true;
            if (clsUserData.GetUserInfoByUserNameAndPassword(UserName, Password,ref PersonID,ref UserID,ref isActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.AddNew;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUserData.isUserExist(UserID);
        }
        public static bool isUserExist(string UserName)
        {
            return clsUserData.isUserExist(UserName);
        }
        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.isUserExistForPersonID(PersonID);
        }
    }
}
