using Newtonsoft.Json;
using System.Numerics;
namespace ProjectManagementApp
{
    public class Manager : User, IManager
    {
        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        public Manager() { }
        //Constructor for creating a new Manager
        public Manager(string username, string password, string firstName, string lastName, string email, string phone)
            : base(username, password, firstName, lastName, email, phone)
        {
            Role = UserRole.Manager;
        }
    }
}
