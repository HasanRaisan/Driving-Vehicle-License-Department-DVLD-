using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using Microsoft.Win32;

namespace DVLD
{
    internal static class clsGlobal
    {
        private static clsUsers _CurrentUser;
        public static clsUsers CurrentUser
        {
            get => _CurrentUser;
            set
            {                
                _CurrentUser = value;
                DVLD_Shared.ClsGlobal.CurrentUserID = _CurrentUser.UserID;
            }
        }

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#" + Password;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool RememberUsernameAndPassword2(string Username, string Password)
        {
            string valueName = "Login";

            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        string deleteKeyPath = @"Software\DVLD";
                        using (RegistryKey key = baseKey.OpenSubKey(deleteKeyPath, true))
                        {
                            if (key != null)
                            {
                                key.DeleteValue(valueName);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    string storedData = (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\DVLD", valueName, null);

                    if (storedData != null)
                    {
                        string[] storedParts = storedData.Split(new string[] { "#//#" }, StringSplitOptions.None);
                        string storedUsername = storedParts[0];
                        string storedPassword = storedParts[1];

                        if (storedUsername == Username && storedPassword == EncryptPassword(Password))
                        {
                            return true;
                        }
                    }

                    string encryptedPassword = EncryptPassword(Password);
                    string dataToSave = Username + "#//#" + encryptedPassword;

                    string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";
                    Registry.SetValue(keyPath, valueName, dataToSave);
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
               // handle Exception
            }
            catch (Exception ex)
            {
                // handle Exception
            }

            return false;
        }

        private static string EncryptPassword(string password)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("0123456789abcdef");
                aesAlg.IV = Encoding.UTF8.GetBytes("1234567890abcdef");

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(password), 0, password.Length);
                return Convert.ToBase64String(encrypted);
            }
        }

        private static string DecryptPassword(string encryptedPassword)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes("0123456789abcdef");
                aesAlg.IV = Encoding.UTF8.GetBytes("1234567890abcdef");

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] decrypted = decryptor.TransformFinalBlock(Convert.FromBase64String(encryptedPassword), 0, Convert.FromBase64String(encryptedPassword).Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }

        public static bool GetStoredCredential2(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD";
            string valueName = "Login";
            try
            {
                string valueDate = Registry.GetValue(keyPath, valueName, null) as string;
                if (valueDate == null) return false;

                string[] result = valueDate.Split(new string[] { "#//#" }, StringSplitOptions.None);
                Username = result[0];
                Password = DecryptPassword(result[1]);
                return true;

            }
            catch (Exception ex)
            {
                // handle Exception
            }
            return false;
        }
    }

}
