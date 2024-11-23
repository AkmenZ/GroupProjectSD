using System;

namespace ProjectManagementApp
{
    public interface ITask
    {
        string TaskID { get; }
        string Title { get; }
        string Description { get; }
        string AssignedTo { get; }
        TaskStatus Status { get; }
        int ProjectID { get; }
        bool StartTask(string currentUsername);
        bool CompleteTask(string currentUsername);
        bool UpdateStatus(TaskStatus newStatus);
        void AssignUser(string username);
    }
}
