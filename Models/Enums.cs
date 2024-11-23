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
        ToDo,
        InProgress,
        Completed,
        Blocked
    }
}
