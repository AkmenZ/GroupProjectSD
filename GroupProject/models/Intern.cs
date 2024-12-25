using Newtonsoft.Json;
namespace ProjectManagementApp
{
    public class Intern : User, IIntern
    {
        //Properties unique to Intern     
        [JsonProperty]
        public string MentorUsername { get; private set; }
        //Parameterless constructor for JSON deserialization
        public Intern() { }
        //Constructor for creating a new Intern
        public Intern(string username, string password, string firstName, string lastName, string email, string phone, string mentorUsername)
            : base(username, password, firstName, lastName, email, phone)
        {
            Role = UserRole.Intern;           
            MentorUsername = mentorUsername;
        }
    }
}
