using System.Collections.Generic;

namespace ProjectManagementApp
{
    public interface IProjectsService
    {
        bool AddProject(string name, string manager, string description);
        bool AddTeamMember(int projectID, string username);
        bool UpdateStatus(int projectID, ProjectStatus status);
        bool DeleteProject(int projectID);
        Project GetProjectById(int projectID);
        IReadOnlyList<Project> GetProjectsByUser(string username);
        IReadOnlyList<Project> GetProjectsByManager(string manager);
        IReadOnlyList<Project> GetProjectsByStatus(ProjectStatus status);
        IReadOnlyList<Project> GetAllProjects();
    }
}