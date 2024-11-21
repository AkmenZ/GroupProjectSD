public abstract class User : IMenu
{
    // props
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int EmployeeId { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    protected readonly TaskService TaskService;

    // constructro
    protected User(string firstName, string lastName, int employeeId, string email, string password, string role, TaskService taskService)
    {
        FirstName = firstName;
        LastName = lastName;
        EmployeeId = employeeId;
        Email = email;
        Password = password;
        Role = role;
        TaskService = taskService;
    }

    // methods
    public virtual void ViewTasks()
    {
        var tasks = TaskService.LoadTasks();
        Console.WriteLine("\nTasks:");
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id}, Title: {task.Title}, Description: {task.Description}");
        }
    }

    // abstract method to handle menu that will need to be implemented in derived classes
    public abstract void HandleMenu();
}

