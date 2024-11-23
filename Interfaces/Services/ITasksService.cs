using System.Collections.Generic;

namespace ProjectManagementApp
{
    public interface ITasksService
    {
        bool AddTask(string title, string description, string assignedTo, int projectID);
        bool AssignUserToTask(string taskId, string username);
        bool StartTask(string taskId, string currentUsername);
        bool StopTask(string taskID, string currentUsername);
        bool UpdateTaskStatus(string taskId, TaskStatus status);
        bool DeleteTask(string taskId);
        Task GetTaskById(string taskId);
        IReadOnlyList<Task> GetUserTasks(string username);
        IReadOnlyList<Task> GetAllTasks();
    }
}
