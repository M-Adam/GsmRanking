using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GsmRanking.Common
{
    public static class Hasher
    {
        public static string HashPassword(string password)
        {
            var passwordByte = Encoding.UTF8.GetBytes(password);
            string result;
            using (var sha = SHA256.Create())
            {
                var hashedBytes = sha.ComputeHash(passwordByte).ToArray();
                result = BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
            return result;
        }

        public static bool IsPasswordValid(string storedPassword, string givenPassword)
        {
            var givenPasswordHash = HashPassword(givenPassword);
            return givenPasswordHash == storedPassword;
        }
    }
}