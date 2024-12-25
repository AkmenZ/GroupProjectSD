using Newtonsoft.Json;
using System.Data;

namespace ProjectManagementApp
{
    public class TeamMember : User, ITeamMember 
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
        [JsonProperty]
        public string Skills { get; private set; }

        //Parameterless constructor for JSON deserialization
        [JsonConstructor]
        public TeamMember() { }
        //Constructor for creating a new TeamMember
        public TeamMember(string username, string password, string firstName, string lastName, string email, string phone, string skills)
            : base(username, password, firstName, lastName, email, phone)
        {
            Role = UserRole.TeamMember;
            Skills = skills;
        }
    }
}