using System;
using Newtonsoft.Json;

namespace ProjectManagementApp
{
    public abstract class User
    {
        //Properties common to all users
        [JsonProperty]
        public string Username { get; protected set; }
        [JsonProperty]
        private string PasswordHash { get; set; }
        [JsonProperty]
        private byte[] PasswordSalt { get; set; }
        [JsonProperty]
        public UserRole Role { get; protected set; }

        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        public User() { }
        //Constructor for creating a new user
        public User(string username, string password, UserRole role)
        {
            Username = username;
            SetPassword(password);
            Role = role;
        }

        public void SetPassword(string password)
        {
            PasswordSalt = PasswordService.GenerateSalt();
            PasswordHash = PasswordService.HashPassword(password, PasswordSalt);
        }

        public bool VerifyPassword(string password)
        {
            return PasswordService.VerifyPassword(password, PasswordHash, PasswordSalt);
        }
    }
}
