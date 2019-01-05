using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eureka.Controllers;
using System.Security.Cryptography;
using System.Text;

namespace Eureka.Extensions
{
    public static class HelperExtensions
    {      

        public static string NombreDelMes(int mes)
        {
            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre","Noviembre", "Diciembre" };
            return meses[mes-1];
        }

        public static string ObtenerContrasenaHashed(string password) {
            string hashPassword = "";
            using (MD5 md5Hash = MD5.Create())
            {
                hashPassword = GetMd5Hash(md5Hash, password);
            }
            return hashPassword;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {         
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
         
            StringBuilder sBuilder = new StringBuilder();
           
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }          
            return sBuilder.ToString();
        }

        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
           
            string hashOfInput = GetMd5Hash(md5Hash, input);
            
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))            
                return true;
            
            else            
                return false;

            
        }

        public static string HashSHA1(string value)
        {
            var sha1 = SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        
        public static bool isValido(this string value)
        {         
           try {
                var addr = new System.Net.Mail.MailAddress(value);
                return addr.Address == value;
            }
            catch {
                return false;
            }
        }
    }
}
