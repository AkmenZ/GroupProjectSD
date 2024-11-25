using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp
{
    public class ProjectsService : IProjectsService
    {
        //List to store projects
        private List<Project> _projects;
        //Property to store project repository
        private readonly IRepository<Project> _projectRepository;
        //Properties to store dependencies
        private readonly ILoggerService _loggerService;

        //Constructor, dependency injection
        public ProjectsService(IRepository<Project> projectRepository, ILoggerService loggerService)
        {
            _projectRepository = projectRepository;
            _loggerService = loggerService;            
            _projects = _projectRepository.GetAll();
        }

        //Add project
        //Need to consider business logic for duplicate project names, currently not allowed
        public bool AddProject(string name, string manager, string description)
        {
            try
            {
                //Get data
                _projects = _projectRepository.GetAll();
                //Check if project name already exists
                if (_projects.Any(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception($"Project: {name} already exists.");                    
                }
                //Determine the next project ID
                int nextProjectID = _projects.Select(p => p.ProjectID).DefaultIfEmpty(0).Max() + 1;
                //Create new project
                var project = new Project(nextProjectID, name, manager, description);
                //Add project to the projects list
                _projects.Add(project);
                //Save changes
                _projectRepository.SaveAll(_projects);
                //Log action
                _loggerService.LogAction("Project", project.ProjectID.ToString(), "added");                              
                //Return true when project added
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Add Project: {ex.Message}");
            }
        }

        //Add team member to project
        public bool AddTeamMember(int projectID, string username)
        {
            try
            {
                //Get data
                _projects = _projectRepository.GetAll();
                //Find project by ID
                var project = GetProjectById(projectID);                
                //Add team member to project
                project.AddTeamMember(username);
                //Save changes
                _projectRepository.SaveAll(_projects);
                //Log action
                _loggerService.LogAction("Project", project.ProjectID.ToString(), $"Team Member added: {username}");
                //Return true when team member added
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Add Team Member: {ex.Message}");
            }
        }

        //Manually update project status
        //Need to consider business logic for automatic updates when all tasks are completed etc.
        public bool UpdateStatus(int projectID, ProjectStatus status)
        {
            try
            {
                //Get data
                _projects = _projectRepository.GetAll();
                //Find project by ID
                var project = GetProjectById(projectID);                
                //Update project status
                project.UpdateStatus(status);
                //Save changes
                _projectRepository.SaveAll(_projects);
                //Log action
                _loggerService.LogAction("Project", project.ProjectID.ToString(), $"Status Update to: {status}");                
                //Return true when status updated
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Update Project Status: {ex.Message}");                
            }
        }

        //Delete project
        //We have to consider business logic for deleting project with tasks
        public bool DeleteProject(int projectID)
        {
            try
            {
                //Get data
                _projects = _projectRepository.GetAll();
                //Find and assign project to delete
                var project = GetProjectById(projectID);                
                //Delete project
                _projects.Remove(project);
                //Save changes
                _projectRepository.SaveAll(_projects);
                //Log action
                _loggerService.LogAction("Project", project.ProjectID.ToString(), "Deleted");                
                //Return true when project deleted
                return true;
            }
            catch (Exception ex) 
            {
                throw new Exception($"\n  Error Delete Project: {ex.Message}");
            }
        }

        //Get project by ID
        public Project GetProjectById(int projectID)
        {
            try
            {
                var project = _projects.FirstOrDefault(p => p.ProjectID == projectID);
                if (project == null)
                {
                    throw new Exception($"Project with ID: {projectID} not found.");
                }

                return project;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Get Project by ID: {ex.Message}");

            }
        }

        //Get manager projects
        public IReadOnlyList<Project> GetProjectsByUser(string username)
        {
            try
            {                
                //Filter projects by manager                
                return _projects.Where(p => p.Manager.Equals(username, StringComparison.OrdinalIgnoreCase))
                                .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Get User Projects: {ex.Message}");
            }
        }

        //Get projects by manager
        public IReadOnlyList<Project> GetProjectsByManager(string manager)
        {
            try
            {
                //Filter projects by manager
                return _projects.Where(p => p.Manager.Equals(manager, StringComparison.OrdinalIgnoreCase))
                                .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Get Projects by Manager: {ex.Message}");
            }
        }

        //Get projects by status
        public IReadOnlyList<Project> GetProjectsByStatus(ProjectStatus status)
        {
            try
            {
                //Filter projects by status
                return _projects.Where(p => p.Status == status).ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Get Projects by Status: {ex.Message}");
            }
        }



        //Get all projects, return as read-only list
        public IReadOnlyList<Project> GetAllProjects()
        {
            try
            {
                return _projects.AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Get All Projects: {ex.Message}");
            }
        }
    }
}
