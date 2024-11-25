using System.Collections.Generic;

namespace ProjectManagementApp
{
    public interface ITasksService
    {
        bool AddTask(string title, string description, string assignedTo, int projectID);
        bool AssignUserToTask(string taskId, string username);
        bool StartTask(string taskId);
        bool CompleteTask(string taskID);
        bool UpdateTaskStatus(string taskId, TaskStatus status);
        bool DeleteTask(string taskId);
        bool VerifyOwnership(string taskID);                  
        Task GetTaskById(string taskId);
        IReadOnlyList<Task> GetUserTasks(string username);
        IReadOnlyList<Task> GetProjectTasks(int projectID);
        IReadOnlyList<Task> GetProjectTasksByStatus(int projectID, TaskStatus status);
        IReadOnlyList<Task> GetAllTasks();
    }
}
