// this is abstract class that others will derive from
public abstract class User
{
    // props
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public int EmployeeId { get; set;}

    // constructor
    protected User(string firstName, string lastName, int employeeId)
    {
        FirstName = firstName;
        LastName = lastName;
        EmployeeId = employeeId;
    }
}