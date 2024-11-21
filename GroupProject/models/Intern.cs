public class Intern : User
{
    // props
    // TODO (some intern specific props, if any)

    // constructor
    public Intern(string firstName, string lastName, int employeeId, string email, string password, string role, TaskService taskService)
        : base(firstName, lastName, employeeId, email, password, role, taskService)
    {
    }

    // override base class with different behaviour (polimorphism)
    public override void ViewTasks()
    {
        var tasks = TaskService.LoadTasks();
        Console.WriteLine($"\nTasks Assigned to {FirstName}:");
        foreach (var task in tasks)
        {
            if (task.AssignedTo == EmployeeId)
            {
                Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Description: {task.Description}");
            }
        }
    }

    // required interface implementation from base class
    public override void HandleMenu()
    {
        while (true)
        {
            Console.WriteLine("\nIntern Menu:");
            Console.WriteLine("1 - View Your Tasks");
            Console.WriteLine("2 - Logout");
            Console.Write("Enter option: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    ViewTasks();
                    break;
                case "2":
                    Console.WriteLine("Logging out!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again!");
                    break;
            }
        }
    }

    // methods
    // TODO (other intern method)
}
