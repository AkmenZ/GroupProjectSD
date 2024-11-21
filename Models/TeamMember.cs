using Newtonsoft.Json;
using System.Data;

namespace ProjectManagementApp
{
    public class TeamMember : User
    {
        //Properties unique to TeamMember
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
        public TeamMember() { }
        //Constructor for creating a new TeamMember
        public TeamMember(string username, string password, string FirsName, string LastName, string Email, string Phone)
            : base(username, password, UserRole.TeamMember)
        {
        }
    }
}