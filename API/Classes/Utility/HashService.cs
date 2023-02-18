using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace API.Classes.Utility
{
    public static class HashService
    {
        public static string HashString(string stringToHash)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            byte[] bytToHash = System.Text.Encoding.UTF8.GetBytes(stringToHash);
            byte[] bytHash = algorithm.ComputeHash(bytToHash);
            return Convert.ToBase64String(bytHash);
        }
        public static string CreateSalt()
        {
            RNGCryptoServiceProvider Randomizer = new RNGCryptoServiceProvider();
            byte[] Salt = new byte[16];
            Randomizer.GetBytes(Salt);
            return Convert.ToBase64String(Salt);
        }
    }
}
