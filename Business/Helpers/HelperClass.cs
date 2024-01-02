using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;
using System.Web;
using XSystem.Security.Cryptography;

namespace Business.Helpers
{
    public class HelperClass
    {
        private readonly IConfiguration _config;
        private readonly string _securityKey;
        public HelperClass(IConfiguration config)
        {
            _config = config;
            _securityKey = _config["OracleConnection:Key"];
        }
        public string SolutionInfiniSend(int accId, string pNumber, string msg)
        {
            string requestUriString = string.Empty;
            if (accId == 1)
            {
                requestUriString = string.Concat(new string[]
                {
                    "http://bankalerts.sinfini.com/api/web2sms.php?workingkey=Aabf003763d4a95672832f21e2e0725ed&sender=MAFILR&to= ",
                    Convert.ToString(pNumber),
                    " &message=",
                    HttpUtility.UrlEncode(msg),
                    "&type=xml"
                });
            }
            else if (accId == 2)
            {
                requestUriString = string.Concat(new string[]
                {
                    "http://bankalerts.sinfini.com/api/web2sms.php?workingkey=Ab495a7783699c0b49b130a5924fcebef&sender=MAFILT&to= ",
                    Convert.ToString(pNumber),
                    " &message=",
                    HttpUtility.UrlEncode(msg),
                    "&type=xml"
                });
            }
            else if (accId == 3)
            {
                requestUriString = string.Concat(new string[]
                {
                    "http://bankalerts.sinfini.com/api/web2sms.php?workingkey=Ad54e7d5d3d20a7fb840f358a2e959a91&sender=MAFILD&to= ",
                    Convert.ToString(pNumber),
                    " &message=",
                    HttpUtility.UrlEncode(msg),
                    "&type=xml"
                });
            }
            else if (accId == 4)
            {
                requestUriString = string.Concat(new string[]
                {
                    "http://bankalerts.sinfini.com/api/web2sms.php?workingkey=A98121438b7df1bd359b083edcfedffaa&sender=MAFLHR&to= ",
                    Convert.ToString(pNumber),
                    " &message=",
                    HttpUtility.UrlEncode(msg),
                    "&type=xml"
                });
            }

            WebRequest webRequest = WebRequest.Create(requestUriString);
            Stream responseStream = webRequest.GetResponse().GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            string text = streamReader.ReadLine();

            if (text == null || text.Length == 0)
            {
                return text = "No Message";
            }
            else
            {
                return text;
            }
        }

        public string GenerateOTP(int Length)
        {
            string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            Random randNum = new Random();
            char[] chars = new char[Length];

            for (int i = 0; i < Length; i++)
            {
                chars[i] = _allowedChars[Convert.ToInt32((_allowedChars.Length - 1) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public byte[] getEdata(string userName, string password)
        {            
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            UTF8Encoding uTF8Encoding = new UTF8Encoding();
            return mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(userName + _securityKey + password));
        }
    }
}
