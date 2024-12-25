using System;
using System.Numerics;
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
        [JsonProperty]
        public string FirstName { get; private set; }
        [JsonProperty]
        public string LastName { get; private set; }
        [JsonProperty]
        public string Email { get; private set; }
        [JsonProperty]
        public string Phone { get; private set; }

        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        public User() { }
       
        public User(string username, string password, string firstName, string lastName, string email, string phone)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
        

        //Update the password hash and salt
        public void SetHashSalt(string passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
