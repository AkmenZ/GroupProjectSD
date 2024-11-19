// parent class, every type of user will be able to view tasks
using System.Text.Json;

public class UserService
{
    // props
    protected string TasksFilePath;
    // constructor
    public UserService(string tasksFilePath)
    {
        TasksFilePath = tasksFilePath;
    }
    // methods
    public List<Task> LoadTasks()
    {
        // if file doesnt exist, create and initialize to empty json file
        if (!File.Exists(TasksFilePath))
        {
            File.WriteAllText(TasksFilePath, "[]");
        }

        string json = File.ReadAllText(TasksFilePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Task>();
        }
        try
        {
            return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
        }
        catch (JsonException)
        {
            // resets file to empty file if there is exception
            Console.WriteLine("Invalid JSON format in tasks file!");
            File.WriteAllText(TasksFilePath, "[]");
            return new List<Task>();
        }
    }
    public void ViewTasks()
    {
        var tasks = LoadTasks();
        Console.WriteLine("\nTasks:");
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
    }
}