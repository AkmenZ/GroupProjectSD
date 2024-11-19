// this is abstract class that others will derive from
public abstract class User
{
    // props
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public int EmployeeId { get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string Role {get; set;}

    // constructor
    protected User(string firstName, string lastName, int employeeId, string email, string password, string role)
    {
        FirstName = firstName;
        LastName = lastName;
        EmployeeId = employeeId;
        Email = email;
        Password = password;
        Role = role;
    }
}