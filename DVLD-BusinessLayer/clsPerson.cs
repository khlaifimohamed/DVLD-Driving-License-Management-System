using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DVLD_BusinessLayer
{
    public  class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }
        public string NationalNo { get; set; }

        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public string Email { get; set; }
        public int NationalCountryID { get; set; }

        public clsCountry CountryInfo;
        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalCountryID = -1;
            this.ImagePath = "";
            this.Mode = enMode.AddNew;

        }

        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName, string NationalNo,
            string LastName, DateTime DateOfBirth, short Gender, string Address,
             string Phone, string Email, int CountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.NationalNo = NationalNo;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalCountryID = CountryID;
            this.CountryInfo = clsCountry.Find(CountryID);
            this.ImagePath = ImagePath;
            this.Mode = enMode.Update;

        }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.NationalNo
                , this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalCountryID, this.ImagePath);
            return this.PersonID != -1;
        }


        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName, this.NationalNo
                , this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalCountryID, this.ImagePath);
        }




        public static clsPerson Find(int PersonID)
        {
            string FirstName = " ";
            string SecondName = " ";
            string ThirdName = " ";
            string LastName = " ";
            string NationalNo = "";
            DateTime DateOfBirth = DateTime.Now;
            short gender = 0;
            string address = "";
            string Phone = "";
            string Email = "";
            int CountryID = -1;
            string ImagePath = "";
            bool isFound = clsPersonData.GetPersonByID(PersonID, ref FirstName, ref SecondName, ref ThirdName
                , ref NationalNo, ref LastName, ref DateOfBirth, ref gender, ref address, ref Phone, ref Email
                , ref CountryID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName
                , NationalNo, LastName, DateOfBirth, gender, address, Phone, Email
                , CountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }


        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = " ";
            string SecondName = " ";
            string ThirdName = " ";
            string LastName = " ";
            DateTime DateOfBirth = DateTime.Now;
            short gender = 0;
            string address = "";
            string Phone = "";
            string Email = "";
            int CountryID = -1;
            string ImagePath = "";
            bool isFound = clsPersonData.GetPersonByNationalNo(NationalNo,ref  PersonID, ref FirstName, ref SecondName, ref ThirdName
                , ref LastName, ref DateOfBirth, ref gender, ref address, ref Phone, ref Email
                , ref CountryID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(PersonID, FirstName, SecondName, ThirdName
                , NationalNo, LastName, DateOfBirth, gender, address, Phone, Email
                , CountryID, ImagePath);
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
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }


        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }


        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID); 
        }
        public static bool IsPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }
        public static bool IsPersonExist(string NationaNo)
        {
            return clsPersonData.IsPersonExist(NationaNo);
        }

    }
}
