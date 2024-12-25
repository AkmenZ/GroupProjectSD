using System;

namespace ProjectManagementApp
{
    public interface ITask
    {
        string TaskID { get; }
        TaskType Type { get; }
        string EpicID{ get; }
        string Title { get; }
        string Description { get; }
        TaskPriority Priority { get; }
        string AssignedTo { get; }
        TaskStatus Status { get; }
        int ProjectID { get; }
        Double? EstimatedStoryPoints { get; }
        DateTime CreatedAt { get; }
        DateTime? DueDate { get; }
        DateTime? CompletedAt { get; }
        Double? LoggedHours { get; }

        bool StartTask(string currentUsername);
        bool CompleteTask(string currentUsername);
        bool UpdateStatus(TaskStatus newStatus);
        void AssignUser(string username);
        void AssignToEpic(string epicID);   
    }
}
