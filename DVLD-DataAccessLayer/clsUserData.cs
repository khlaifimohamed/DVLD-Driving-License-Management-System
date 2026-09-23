using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    public  class clsUserData
    {

        public static bool GetUserInfoByUserID(int UserID,ref int PersonID,ref string UserName,ref string Password,ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT * from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    isActive = (bool)reader["isActive"];
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
        public static bool GetUserInfoByPersonID(int PersonID,ref int UserID,ref string UserName,ref string Password,ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT * from Users where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    isActive = (bool)reader["isActive"];
                    isFound = true;
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
            return isFound;
            
        }

        public static bool GetUserInfoByUserNameAndPassword(string UserName,string Password,ref int PersonID , ref int UserID, ref bool isActive)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT * from Users where UserName = @UserName and Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    PersonID = (int)reader["PersonID"];
                    UserID = (int)reader["UserID"];
                    isActive = (bool)reader["isActive"];
                    isFound = true;
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
            return isFound;

        }
       
        public static int AddNewUser(int PersonID,string UserName,string Password,bool isActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = @"Insert into Users(PersonID,UserName,Password,isActive) values(@PersonID,@UserName,@Password,@isActive); SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return UserID;
        }

        public static bool UpdateUser(int UserID,int PersonID,string UserName,string Password,bool isActive)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = @"Update Users Set UserName = @UserName,PersonId = @PersonID,Password = @Password,isActive = @isActive where UserID = @UserID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string Query = "SELECT Users.UserID ,Users.PersonID , FullName = People.FirstName + ' '+ People.SecondName + ' ' + ISNULL( People.ThirdName,'') + ' ' + People.LastName,Users.UserName, Users.IsActive  " +
                " from Users inner join People on Users.PersonID = People.PersonID ";
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

        public static bool DeleteUser(int UserID)
        {
            int affectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = @"Delete from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return affectedRows > 0;
        }

        public static bool isUserExist(int UserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT Found=1 from Users where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
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
            return isFound;
        }
        public static bool isUserExist(string UserName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT Found=1 from Users where UserName = @UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
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
            return isFound;
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = "SELECT Found=1 from Users where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
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
            return isFound;
        }

        public static bool ChangePassword(int UserID,string Password)
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessConnectionSettings.ConnectionString);
            string query = @"Update Users set PassWord = @Password where UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                RowsAffected = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return RowsAffected > 0;
        }

    }
}
