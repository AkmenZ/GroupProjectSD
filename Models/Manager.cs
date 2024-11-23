using Newtonsoft.Json;
namespace ProjectManagementApp
{
    public class Manager : User, IManager
    {
        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        public Manager() { }
        //Constructor for creating a new Manager
        public Manager(string username, string password)
            : base(username, password, UserRole.Manager)
        {
        }
    }
}
