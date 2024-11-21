using System.Text.Json;

public class Manager : User
{
    public string Department { get; set; }
    private readonly AuthService AuthService;

    public Manager(string firstName, string lastName, int employeeId, string email, string password, string role, string department, TaskService taskService, AuthService authService)
        : base(firstName, lastName, employeeId, email, password, role, taskService)
    {
        Department = department;
        AuthService = authService;
    }

    // required interface implementation from base class
    public override void HandleMenu()
    {
        while (true)
        {
            Console.WriteLine("\nManager Menu:");
            Console.WriteLine("1 - Add Task");
            Console.WriteLine("2 - View All Tasks");
            Console.WriteLine("3 - Assign Task to User");
            Console.WriteLine("4 - Logout");
            Console.Write("Enter option: ");
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    AssignTaskToUser();
                    break;
                case "4":
                    Console.WriteLine("Logging out!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again!");
                    break;
            }
        }
    }

    // methods
    private void AddTask()
    {
        Console.Write("Enter Task Title: ");
        string title = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter Task Description: ");
        string description = Console.ReadLine() ?? string.Empty;

        var tasks = TaskService.LoadTasks();
        int newId = tasks.Count > 0 ? tasks[^1].Id + 1 : 1;
        TaskService.AddTask(new Task(newId, title, description));

        Console.WriteLine("Task added successfully!");
    }

    private void AssignTaskToUser()
    {
        // gets and lists all unassigned tasks
        var tasks = TaskService.LoadTasks();
        var unassignedTasks = tasks.Where(task => task.AssignedTo == null).ToList();

        // return if no such tasks found
        if (!unassignedTasks.Any())
        {
            Console.WriteLine("No unassigned tasks available!");
            return;
        }

        // print out the available tasks
        Console.WriteLine("\nUnassigned Tasks:");
        foreach (var task in unassignedTasks)
        {
            Console.WriteLine($"ID: {task.Id}, Title: {task.Title}");
        }

        // declare task id by parsing it from input
        Console.Write("Enter Task ID to assign: ");
        if (!int.TryParse(Console.ReadLine(), out int taskId))
        {
            Console.WriteLine("Invalid Task ID!");
            return;
        }

        // select the task with that id
        var selectedTask = unassignedTasks.FirstOrDefault(task => task.Id == taskId);
        if (selectedTask == null)
        {
            Console.WriteLine($"Task with ID {taskId} is not available!");
            return;
        }

        // get available users from auth service
        var nonManagerUsers = AuthService.GetNonManagerUsers();

        // return if no users found
        if (!nonManagerUsers.Any())
        {
            Console.WriteLine("No users available for task assignment!");
            return;
        }

        // list out the users
        Console.WriteLine("\nAvailable Users:");
        foreach (var user in nonManagerUsers)
        {
            Console.WriteLine($"Name: {user.GetProperty("FirstName").GetString()} {user.GetProperty("LastName").GetString()}, " +
                              $"Role: {user.GetProperty("Role").GetString()}, " +
                              $"Employee ID: {user.GetProperty("EmployeeId").GetInt32()}");
        }

        // declare employe id by parsing input
        Console.Write("Enter Employee ID to assign the task to: ");
        if (!int.TryParse(Console.ReadLine(), out int employeeId))
        {
            Console.WriteLine("Invalid Employee ID!");
            return;
        }

        // select the user with that id
        var selectedUser = nonManagerUsers.FirstOrDefault(user => user.GetProperty("EmployeeId").GetInt32() == employeeId);
        if (selectedUser.ValueKind == JsonValueKind.Undefined)
        {
            Console.WriteLine($"User with Employee ID {employeeId} not found!");
            return;
        }

        // call update method from task service
        bool updated = TaskService.UpdateTask(taskId, assignedTo: employeeId);

        // print result
        if (updated) 
        {
            // selectedUser is not user object, but a json element, so interpolate it as such
            Console.WriteLine($"Task ID: {taskId} has been assigned to {selectedUser.GetProperty("Role").GetString()} - {selectedUser.GetProperty("FirstName").GetString()} {selectedUser.GetProperty("LastName").GetString()}!");
        }
        else
        {
            Console.WriteLine($"Something went bad!");
        }
    }
}




