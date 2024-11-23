using Newtonsoft.Json;
namespace ProjectManagementApp
{
    public class Administrator : User, IAdministrator
    {
        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        protected Administrator() { }
        //Constructor for creating a new Administrator
        public Administrator(string username, string password)
            : base(username, password, UserRole.Administrator)
        {
        }
    }
}
