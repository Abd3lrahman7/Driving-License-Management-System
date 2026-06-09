using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;


namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_UserLoginInfo";

                string UsernameValueName = "Username";
                string UsernameValueData = Username;

                string PasswordValueName = "Password";
                string PasswordValueData = Password;

                if(string.IsNullOrWhiteSpace(Username))
                {
                    Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\DVLD_UserLoginInfo",false);
                    return true;
                }

                Registry.SetValue(KeyPath, UsernameValueName, UsernameValueData, RegistryValueKind.String);
                Registry.SetValue(KeyPath, PasswordValueName, PasswordValueData, RegistryValueKind.String);

                return true;
            }
            catch (Exception ex)
            {
               MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_UserLoginInfo";
                string UsernameValueName = "Username";
                string PasswordValueName = "Password";

                Username = Registry.GetValue(KeyPath,UsernameValueName,null) as string;
                Password = Registry.GetValue(KeyPath,PasswordValueName,null) as string;

                return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;   
            }

        }
    }
}
