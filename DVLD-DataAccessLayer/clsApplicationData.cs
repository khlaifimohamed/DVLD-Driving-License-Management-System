using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer
{
    public  class clsApplicationData
    {
        public static bool GetApplicationInfoByID(int ApplicationID,ref int ApplicantPersonID,ref DateTime ApplicationDate
            ,ref int ApplicationTypeID,ref byte ApplicationStatus,ref DateTime LastStatusdate
            ,ref float PaidFees,ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT * from Applications where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusdate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    isFound = true;
                }
                reader.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }



        public static int AddNewApplication(int ApplicantPersonID,DateTime ApplicationDate,int ApplicationTypeID,byte ApplicationStatus,DateTime LastStatusDate,float PaidFees,int CreatedByUserID)
        {
            int NewApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = @"insert into Applications(ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
                            values(@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    NewApplicationID = insertedID;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return NewApplicationID;
        }



        public static bool UpdateApplication(int ApplicationID,int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = @"Update Applications set ApplicantPersonID = @ApplicantPersonID,ApplicationDate=@ApplicationDate,ApplcationTypeID = @ApplicationTypeID,ApplicationStatus=@ApplicationStatus,LastStatusDate=@LastStatusDate,PaidFees=@PaidFees,CreatedByUserID=@CreatedByUserID where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return AffectedRows > 0;
        }



        public static bool DeleteApplication(int ApplicationID)
        {
            int AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = @"Delete from Applications where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return AffectedRows > 0;
        }



        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = "SELECT  Found=1 from Applications where ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }


        public static bool DoesPersonHaveActiveApplication(int PersonID,int ApplicationTypeID)
        {
            return GetActiveApplication(PersonID, ApplicationTypeID) != -1;
        }

        public static int GetActiveApplication(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = "SELECT  ActiveApplicationID=ApplicationID from Applications where ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID = @ApplicationTypeID and ApplicationStatus = 1 ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                Object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ActiveApplicationID;
        }


        public static int GetActiveApplicationIDForLicenseClass(int PersonID,int ApplicationTypeID,int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT ActiveApplicationID=Applications.ApplicationID FROM Applications inner join  LocalDrivingLicenseApplications on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID where ApplicantPersonID=@ApplicantPersonID and ApplicationTypeId = @ApplicationTypeID and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID and ApplicationStatus = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ActiveApplicationID;
        }
    
        public static bool UpdateStatus(int ApplicationID , short NewStatus)
        {
            int AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "Update Applications Set ApplicationStatus = @ApplicationStatus,LastStatusDate=@LastStatusDate where  ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return AffectedRows > 0;
        }
    
    
    
    
    }


}
