using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;
namespace DVLD_BusinessLayer
{
    public class clsApplicationType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        public clsApplicationType()
        {
            this.ID = -1;
            this.Title = "";
            this.Fees = 0;
            this.Mode = enMode.AddNew;
        }
        private clsApplicationType(int ApplicationID,string ApplicationTitle,float ApplicationFees)
        {
            this.ID = ApplicationID;
            this.Title = ApplicationTitle;
            this.Fees = ApplicationFees;
            this.Mode = enMode.Update;
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }

        public static clsApplicationType Find(int ID)
        {
            string Title = "";
            float Fees = 0;
            if( clsApplicationTypeData.GetApplicationTypeByID(ID,ref Title,ref Fees))
            {
                return new clsApplicationType(ID, Title, Fees);
            }
            else
            {
                return null;
            }
        }
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
        public bool Save()
        {
            return _UpdateApplicationType();
        }




    }
}
