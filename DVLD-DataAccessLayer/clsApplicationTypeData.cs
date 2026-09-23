using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public  class clsApplicationTypeData
    {
        public static bool GetApplicationTypeByID(int ApplicationTypeID,ref string ApplicationTypeTitle ,ref float ApplicationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = "SELECT * from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle( reader["ApplicationFees"]);
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



        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = "SELECT * from ApplicationTypes order by ApplicationTypeTitle";
            SqlCommand command = new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }


        public static bool UpdateApplicationType(int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationFees)
        {
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = @"Update ApplicationTypes Set ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees where ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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
