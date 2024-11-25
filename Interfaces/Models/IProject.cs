using System;

namespace ProjectManagementApp
{
    public interface IProject
    {
        int ProjectID { get; }
        string Name { get; }
        string Manager { get; }
        List<string> TeamMembers { get; }
        ProjectStatus Status { get; }
        string Description { get; }
        void UpdateStatus(ProjectStatus status);
    }
}
