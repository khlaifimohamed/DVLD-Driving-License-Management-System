using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace DVLD.Classes
{
    internal static class clsGlobal
    {
        public static clsUser CurrentUser;



        public static string HashPassword(string Password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));


                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            //try
            //{
            //    
            //    string currentDirectory = System.IO.Directory.GetCurrentDirectory();


            //   
            //    string filePath = currentDirectory + "\\data.txt";

            //   
            //    if (Username == "" && File.Exists(filePath))
            //    {
            //        File.Delete(filePath);
            //        return true;

            //    }

            //    
            //    string dataToSave = Username + "#//#" + Password;

            //    
            //    using (StreamWriter writer = new StreamWriter(filePath))
            //    {
            //        // Write the data to the file
            //        writer.WriteLine(dataToSave);

            //        return true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"An error occurred: {ex.Message}");
            //    return false;
            //}


            /// this feature too was implemented to work with file text now we should update it to the windows registry
            /// 


            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

            try
            {
                Registry.SetValue(KeyPath, "UserName", Username, RegistryValueKind.String);
                Registry.SetValue(KeyPath, "Password", Password, RegistryValueKind.String);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;



        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            //try
            //{
            //    //gets the current project's directory
            //    string currentDirectory = System.IO.Directory.GetCurrentDirectory();

            //    // Path for the file that contains the credential.
            //    string filePath = currentDirectory + "\\data.txt";

            //    // Check if the file exists before attempting to read it
            //    if (File.Exists(filePath))
            //    {
            //        // Create a StreamReader to read from the file
            //        using (StreamReader reader = new StreamReader(filePath))
            //        {
            //            // Read data line by line until the end of the file
            //            string line;
            //            while ((line = reader.ReadLine()) != null)
            //            {
            //                Console.WriteLine(line); // Output each line of data to the console
            //                string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

            //                Username = result[0];
            //                Password = result[1];
            //            }
            //            return true;
            //        }
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"An error occurred: {ex.Message}");
            //    return false;
            //}






            // this commented code is the old version i was implemented to use the text file . now i am gonna update it to use the windows registry
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";



            try
            {
                string Value1 = Registry.GetValue(KeyPath, "UserName", null) as string;
                string Value2 = Registry.GetValue(KeyPath, "Password", null) as string;
                if(Value1 != null && Value2 != null)
                {
                    Username = Value1;
                    Password = Value2;
                    return true;
                }
                else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }



        }
    }
}
