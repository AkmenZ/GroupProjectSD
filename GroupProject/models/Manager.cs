public class Manager : User
{
    // props
    public string Department {get; set;}
    // constructor
    public Manager(string firstName, string lastName, int employeeId, string department) : base(firstName, lastName, employeeId)
    {
        Department = department;
    }

    // methods
    public void AddTask()
    {
        Console.WriteLine("Adding new task!");
    }
    public void AssignDeveloper()
    {
        Console.WriteLine("Adding task to developer!");
    }
}