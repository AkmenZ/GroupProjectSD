using System;
using System.Security.Cryptography;
using System.Text;

namespace ProjectManagementApp
{
    //https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
    public class PasswordService : IPasswordService
    {
        private const int _keySize = 64;
        private const int _iterations = 350000;
        private static HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;
                
        //Generate random salt
        public byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(_keySize);
        }

        //Hash password using PBKDF2
        public string HashPassword(string password, byte[] salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);
            return Convert.ToHexString(hash);
        }        

        //Verify password
        public bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
