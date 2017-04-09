using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GsmRanking.Common
{
    public static class Hasher
    {
        public static string HashPassword(string password, string username)
        {
            byte[] salt = Encoding.UTF8.GetBytes(username);
            byte[] passwordByte = Encoding.UTF8.GetBytes(password);
            string result;
            using (var sha = SHA256.Create())
            {
                int half = salt.Length / 2;
                var firstHalf = salt.Take(half);
                var secondHalf = salt.Skip(half);
                var hashedBytes = sha.ComputeHash(firstHalf.Concat(passwordByte).Concat(secondHalf).ToArray());
                result = BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
            return result;
        }

        public static bool IsPasswordValid(string username, string storedPassword, string givenPassword)
        {
            var givenPasswordHash = HashPassword(givenPassword, username);
            return givenPasswordHash == storedPassword;
        }
    }
}