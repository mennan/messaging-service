using System;
using System.Security.Cryptography;
using System.Text;

namespace MessagingService.Service
{
    public static class Cryptography
    {
        public static string Encrypt(this string password)
        {
            var message = Encoding.Unicode.GetBytes(password);
            var hash = SHA512.Create();
            var hashValue = hash.ComputeHash(message);
            
            return Encoding.Unicode.GetString(hashValue);
        }
    }
}