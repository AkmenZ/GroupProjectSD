using System;
using Newtonsoft.Json;

namespace ProjectManagementApp
{
    public abstract class User : IUser
    {
        //Properties common to all users
        [JsonProperty]
        public string Username { get; protected set; }
        [JsonProperty]
        private string _PasswordHash { get; set; }
        [JsonProperty]
        private byte[] _PasswordSalt { get; set; }
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
            _PasswordSalt = PasswordService.GenerateSalt();
            _PasswordHash = PasswordService.HashPassword(password, _PasswordSalt);
        }

        public bool VerifyPassword(string password)
        {
            return PasswordService.VerifyPassword(password, _PasswordHash, _PasswordSalt);
        }
    }
}
