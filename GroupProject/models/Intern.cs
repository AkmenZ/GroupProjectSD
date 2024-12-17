using Newtonsoft.Json;
namespace ProjectManagementApp
{
    public class Intern : User, IIntern
    {
        //Parameterless constructor for JSON deserialization
        //Properties unique to TeamMember
        [JsonProperty]
        public string FirstName { get; private set; }
        [JsonProperty]
        public string LastName { get; private set; }
        [JsonProperty]
        public string MentorUsername { get; private set; }
        public Intern() { }
        //Constructor for creating a new Manager
        public Intern(string username, string password, string firstName, string lastName, string mentorUsername)
            : base(username, password)
        {
            Role = UserRole.Intern;
            FirstName = firstName;
            LastName = lastName;
            MentorUsername = mentorUsername;
        }
    }
}
