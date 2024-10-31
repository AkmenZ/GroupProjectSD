public class Manager : User
{
    // constructor
    public Manager(string firstName, string lastName, int employeeId) : base(firstName, lastName, employeeId)
    {
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