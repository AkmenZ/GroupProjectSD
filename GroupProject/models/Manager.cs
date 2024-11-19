public class Manager : User
{
    // props
    public string Department { get; set; }
    // constructor
    public Manager(string firstName, string lastName, int employeeId, string email, string password, string role, string department) : base(firstName, lastName, employeeId, email, password, role)
    {
        Department = department;
    }
}