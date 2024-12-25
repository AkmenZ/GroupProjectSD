using Newtonsoft.Json;
namespace ProjectManagementApp
{
    public class Administrator : User, IAdministrator
    {
        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        protected Administrator() { }
        //Constructor for creating a new Administrator
        public Administrator(string username, string password, string firstName, string lastName, string email, string phone)
                      : base(username, password, firstName, lastName, email, phone)
        {
            Role = UserRole.Administrator;
        }
    }
}
