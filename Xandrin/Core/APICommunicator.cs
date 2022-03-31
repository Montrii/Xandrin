using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

// set to be blurred out entirely for security purposes of user data!

namespace Xandrin.Core
{
    public class APICommunicator
    {


        #region Private Fields
        #endregion

        private const string InitVector = "T=A4rAzu94ez-dra";
        private const int PasswordIterations = 1000; //2;
        private const string SaltValue = "d=?ustAF=UstenAr3B@pRu8=ner5sW&h59_Xe9P2za-eFr2fa&ePHE@ras!a+uc@";

        private Account usedAccount = null;

        public Account UsedAccount { get => usedAccount; set => usedAccount = value; }

        public APICommunicator()
        {
        }


        [DllImport("C:\\Users\\Drago\\source\\repos\\gtafunctions\\x64\\Debug\\gtafunctions.dll")]
        public static extern IntPtr Create();

        [DllImport("C:\\Users\\Drago\\source\\repos\\gtafunctions\\x64\\Debug\\gtafunctions.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        public static extern string getARandomString(IntPtr f);




        public string loginToDatabase(Account account)
        {
            using (var wb = new WebClient())
            {

                var data = new NameValueCollection();

                data["\u0058\u0041\u004e\u0044\u0052\u0049\u004e"] = ComputeSha256Hash(getARandomString(Create()));
                data["username"] = account.Username;
                data["password"] = ComputeSha256Hash(account.Password);

                var responseBytes = wb.UploadValues(
                    "https://montriscript.com/Xandrin/api/database/login.php", "POST", data);

                string responseString = Encoding.Default.GetString(responseBytes);
                if(responseString.Contains("LOGIN SUCCESSFULL - WELCOME BACK") && usedAccount == null)
                {
                    account.IsLoggedIn = 1;
                    account.Password = ComputeSha256Hash(account.Password);
                    usedAccount = account;
                    safeAccountData(account);
                }
                Console.WriteLine(responseString);
                return responseString;
            }
        }


        public void signOutOfDatabase(Account account)
        {
            if(account != null)
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["\u0058\u0041\u004e\u0044\u0052\u0049\u004e"] = ComputeSha256Hash(getARandomString(Create()));
                    data["username"] = account.Username;
                    data["password"] = account.Password;

                    var responseBytes = wb.UploadValues(
                        "https://montriscript.com/Xandrin/api/database/signOut.php", "POST", data);

                    string responseString = Encoding.Default.GetString(responseBytes);
                    Console.WriteLine(responseString);
                }
            }
        }
        private void safeAccountData(Account account)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Xandrin\\loginData.xml";
            XmlSerializer xml = new XmlSerializer(typeof(Account));
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Xandrin\\"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Xandrin\\");
            }
            FileStream file = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Xandrin\\loginData.xml");
            file.Close();
            TextWriter txt = new StreamWriter(path);
            xml.Serialize(txt, account);
            txt.Close();

            addSignatureToXml(path);




        }

        private void addSignatureToXml(string path)
        {
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = "XML_DSIG_RSA_KEY";

            // Create a new RSA signing key and save it in the container.
            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(path);

            SignXml(xmlDoc, rsaKey);

            Console.WriteLine("XML file signed.");

            // Save the document.
            xmlDoc.Save(path);

            Console.WriteLine("Verifying signature...");
            bool result = VerifyXml(xmlDoc, rsaKey);
            if (result)
            {
                Console.WriteLine("The XML signature is valid.");
            }
            else
            {
                Console.WriteLine("The XML signature is not valid.");
            }
        }

        public static Boolean VerifyXml(XmlDocument xmlDoc, RSA key)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (key == null)
                throw new ArgumentException("key");

            // Create a new SignedXml object and pass it
            // the XML document class.
            SignedXml signedXml = new SignedXml(xmlDoc);

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

            // Throw an exception if no signature was found.
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }

            // This example only supports one signature for
            // the entire XML document.  Throw an exception
            // if more than one signature was found.
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            // Load the first <signature> node.
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(key);
        }


        public static void SignXml(XmlDocument xmlDoc, RSA rsaKey)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException(nameof(xmlDoc));
            if (rsaKey == null)
                throw new ArgumentException(nameof(rsaKey));

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = rsaKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }

        public string registerToDatabase(Account account)
        {
            using (var wb = new WebClient())
            {

                var data = new NameValueCollection();

                data["\u0058\u0041\u004e\u0044\u0052\u0049\u004e"] = ComputeSha256Hash(getARandomString(Create()));
                data["username"] = account.Username;
                data["password"] = ComputeSha256Hash(account.Password);

                var responseBytes = wb.UploadValues(
                    "https://montriscript.com/Xandrin/api/database/register.php", "POST", data);

                string responseString = Encoding.Default.GetString(responseBytes);
                return responseString;
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
