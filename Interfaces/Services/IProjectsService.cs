using System.Collections.Generic;

namespace ProjectManagementApp
{
    public interface IProjectsService
    {
        bool AddProject(string name, string manager, string description);
        bool UpdateProjectStatus(int projectID, ProjectStatus status);
        bool DeleteProject(int projectID);
        Project GetProjectById(int projectID);
        IReadOnlyList<Project> GetManagerProjects(string username);
        IReadOnlyList<Project> GetAllProjects();
    }
}