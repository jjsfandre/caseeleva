using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CaseEleva.Models;
using System.Web.Security;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace CaseEleva.Helpers
{
    public class Authentication
    {
        private static string statusLogin;
        private static string nickNamePostData;

        public static bool Login()
        {
            
            return true;
        }

        public static void Logout()
        {
            statusLogin = String.Empty;
            Authentication.DestroySession();
        }

        public static string GenerateRandomPassword(int charSize, int especialCharSize)
        {
            return encryptMd5(Membership.GeneratePassword(charSize, especialCharSize));
        }

        public static void DestroySession()
        {
           
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidNickName(string nickname)
        {
            Regex regex = new Regex(@"^[A-Za-z][A-Za-z0-9_]{5,14}$");
            return regex.Match(nickname).Success;
        }

        public static bool IsValidPassword(string password)
        {
            Regex regex = new Regex(@"^([0-9A-Za-z@._!?]{8,15})$");
            return regex.Match(password).Success;
        }

        private static string encryptMd5 (string password)
        {
            StringBuilder senha = new StringBuilder();

            MD5 md5 = MD5.Create();
            byte[] entrada = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(entrada);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                senha.Append(hash[i].ToString("X2"));
            }
            return senha.ToString();
        }
    }
}