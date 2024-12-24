using System;

namespace ProjectManagementApp
{
    //Different user roles
    public enum UserRole
    {
        TeamMember,
        Manager,
        Intern,
        Administrator,
        System
    }

    //Different project statuses
    public enum ProjectStatus
    {
        Created,
        InProgress,
        Completed,
        OnHold
    }

    //Different task statuses
    public enum TaskStatus
    {
        Backlog,
        ToDo,
        InProgress,
        Completed,
        Blocked
    }

    //Different task types
    public enum TaskType
    {
        Epic,
        Idea,
        Bug,
        Feature,
        Improvement,
        Documentation
    }

    public enum TaskPriority
    {
        Highest,
        High,
        Medium,
        Low,
        Lowest
    }
}
