using System;
using System.Security.Cryptography;
using Xunit;

namespace ProjectManagementApp.Tests
{
    public class PasswordServiceTests
    {
        private readonly PasswordService _passwordService;

        public PasswordServiceTests()
        {
            _passwordService = new PasswordService();
        }

        [Fact]
        public void GenerateSalt_ShouldReturnUniqueSalt()
        {
            // Act
            var salt1 = _passwordService.GenerateSalt();
            var salt2 = _passwordService.GenerateSalt();

            // Assert
            Assert.NotNull(salt1);
            Assert.NotNull(salt2);
            Assert.NotEqual(salt1, salt2); // Each call should return a unique salt
            Assert.Equal(64, salt1.Length); // Ensure the salt length matches the key size
            Assert.Equal(64, salt2.Length);
        }

        [Fact]
        public void HashPassword_ShouldReturnConsistentHashForSameInput()
        {
            // Arrange
            var password = "SecurePassword123!";
            var salt = _passwordService.GenerateSalt();

            // Act
            var hash1 = _passwordService.HashPassword(password, salt);
            var hash2 = _passwordService.HashPassword(password, salt);

            // Assert
            Assert.NotNull(hash1);
            Assert.NotNull(hash2);
            Assert.Equal(hash1, hash2); // Hash should be consistent for the same password and salt
        }

        [Fact]
        public void HashPassword_ShouldReturnDifferentHashesForDifferentSalts()
        {
            // Arrange
            var password = "SecurePassword123!";
            var salt1 = _passwordService.GenerateSalt();
            var salt2 = _passwordService.GenerateSalt();

            // Act
            var hash1 = _passwordService.HashPassword(password, salt1);
            var hash2 = _passwordService.HashPassword(password, salt2);

            // Assert
            Assert.NotNull(hash1);
            Assert.NotNull(hash2);
            Assert.NotEqual(hash1, hash2); // Different salts should produce different hashes
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
        {
            // Arrange
            var password = "SecurePassword123!";
            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(password, salt);

            // Act
            var result = _passwordService.VerifyPassword(password, hash, salt);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
        {
            // Arrange
            var correctPassword = "SecurePassword123!";
            var incorrectPassword = "WrongPassword456!";
            var salt = _passwordService.GenerateSalt();
            var hash = _passwordService.HashPassword(correctPassword, salt);

            // Act
            var result = _passwordService.VerifyPassword(incorrectPassword, hash, salt);

            // Assert
            Assert.False(result);
        }
    }
}
