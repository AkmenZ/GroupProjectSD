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
        public string PasswordHash { get; private set; }
        [JsonProperty]
        public byte[] PasswordSalt { get; private set; }
        [JsonProperty]
        public UserRole Role { get; protected set; }

        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        public User() { }
       
        public User(string username, string password)
        {
            Username = username;                   
        }
        

        //Update the password hash and salt
        public void SetHashSalt(string passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
