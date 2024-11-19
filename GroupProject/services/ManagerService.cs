using System.Text.Json;

public class ManagerService : UserService
{
    // props
    private Manager Manager;
    // constructor
    public ManagerService(Manager manager, string tasksFilePath) : base(tasksFilePath)
    {
        Manager = manager;
    }
    // methods
    public void HandleManagerOptions()
    {
        while (true)
        {
            Console.WriteLine("\nManager Menu:");
            Console.WriteLine("1 - Add Task");
            Console.WriteLine("2 - View All Tasks");
            Console.WriteLine("3 - Assign To User");
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
                    AssignUserToTask();
                    break;
                case "4":
                    Console.WriteLine("You logged out!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again!");
                    break;
            }
        }
    }

    private void AddTask()
    {
        Console.Write("Enter Title: ");
        string title = Console.ReadLine() ?? string.Empty;
        Console.Write("Enter Description: ");
        string description = Console.ReadLine() ?? string.Empty;

        var tasks = LoadTasks();
        // get the id of last task, increment and assign to new task
        int newId = tasks.Count > 0 ? tasks[^1].Id + 1 : 1;

        tasks.Add(new Task(newId, title, description));
        SaveTasks(tasks);
        Console.WriteLine("Task added successfully!");
    }

    private void SaveTasks(List<Task> tasks)
    {
        // write task to file
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(TasksFilePath, json);
    }

    private void AssignUserToTask() {
        // TODO (logic for assigning tasks to user)
        // list the tasks
        // list non manager users
        // pick the task id, update assignedTo
        // ???
        Console.WriteLine("Assinging user to task");
    }
}