using System;
using System.Security.Cryptography;
using System.Text;

namespace Twitter.OAuth.Helpers
{
    internal static class CryptoHelper
    {
        public static string HMACSHA1Hash(string key, string baseSignature)
        {
            using (HMACSHA1 algorithm = new HMACSHA1(new ASCIIEncoding().GetBytes(key)))
            {
                return Convert.ToBase64String(algorithm.ComputeHash(new ASCIIEncoding().GetBytes(baseSignature)));
            }
        }
    }
}