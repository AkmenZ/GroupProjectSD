using System.Text.Json;

public class TaskService
{
    // props
    private readonly string TasksFilePath;

    // constructor
    public TaskService(string tasksFilePath)
    {
        TasksFilePath = tasksFilePath;
    }

    // methods
    // load all tasks
    public List<Task> LoadTasks()
    {
        if (!File.Exists(TasksFilePath))
        {
            return new List<Task>();
        }

        string json = File.ReadAllText(TasksFilePath);
        return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
    }

    // save tasks
    public void SaveTasks(List<Task> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(TasksFilePath, json);
    }

    // add a new task
    public void AddTask(Task newTask)
    {
        var tasks = LoadTasks();
        tasks.Add(newTask);
        SaveTasks(tasks);
    }

    // update task, allow null values to update only what is required
    public bool UpdateTask(int taskId, int? assignedTo = null, string? status = null, string? description = null)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == taskId);
        if (task == null)
        {
            Console.WriteLine($"Task with ID {taskId} not found!");
            return false;
        }
        // assign the updates
        if (assignedTo != null) task.AssignedTo = assignedTo;
        if (status != null) task.Status = status;
        if (description != null) task.Description = description;

        SaveTasks(tasks); // call save
        return true;
    }

}
