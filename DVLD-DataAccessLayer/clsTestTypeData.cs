using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public  class clsTestTypeData
    {
        public static bool GetTestTypeInfoByID(int testTypeID ,ref string testTypeTitle,ref string testTypeDescription,ref float testTypeFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT * FROM  TestTypes where TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", testTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    testTypeTitle = (string)reader["TestTypeTitle"];
                    testTypeDescription = (string)reader["TestTypeDescription"];
                    testTypeFees = Convert.ToSingle(reader["TestTypeFees"]);
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


        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT * from TestTypes";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
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
            return dt;
        }

        public static bool UpdateTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,float TestTypeFees)
        {
            int AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = @"Update TestTypes set TestTypeTitle = @TestTypeTitle , TestTypeDescription=@TestTypeDescription, TestTypeFees = @TestTypeFees where TestTypeID = @TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
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
