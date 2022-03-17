using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Xandrin.Core
{
    public class APICommunicator
    {
        public APICommunicator()
        {

        }


        public void loginToDatabase(string username, string password)
        {
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["api_method"] = "Dominik du kek";
                data["username"] = username;
                data["password"] = ComputeSha256Hash(password);
                Console.WriteLine("Password : " + ComputeSha256Hash(password));

                var responseBytes = wb.UploadValues(
                    "https://montriscript.com/Xandrin/api/database/login.php", "POST", data);

                string responseString = Encoding.Default.GetString(responseBytes);
                Console.WriteLine("Received Password from PHP: " + responseString);
            }
        }

        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
