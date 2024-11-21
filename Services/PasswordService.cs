using System;
using System.Security.Cryptography;
using System.Text;

namespace ProjectManagementApp
{
    //https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
    public static class PasswordService
    {
        const int keySize = 64;
        const int iterations = 350000;
        static HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        //Generate random salt
        public static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(keySize);
        }

        //Hash password using PBKDF2
        public static string HashPassword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return Convert.ToHexString(hash);
        }

        //Verify password
        public static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                hashAlgorithm,
                keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
