using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp
{
    public class ProjectsService
    {
        //List to store projects
        private List<Project> _projects = new();
        //Property to store project repository
        private readonly DataRepository<Project> _projectRepository;
        //Properties to store dependencies
        private LoggerService _loggerService;
        private AuthService _authService;

        //Constructor, dependency injection
        public ProjectsService(DataRepository<Project> projectRepository, LoggerService loggerService, AuthService authService)
        {
            _projectRepository = projectRepository;
            _loggerService = loggerService;
            _authService = authService;
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
                //Create new project
                var project = new Project(name, manager, description);
                //Add project to the projects list
                _projects.Add(project);
                //Save changes
                _projectRepository.SaveAll(_projects);
                //Log action
                _loggerService.LogAction($"Project added ID: {project.ProjectID} : {name}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
                //Return true when project added
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Add Project: {ex.Message}");
            }
        }

        //Manually update project status
        //Need to consider business logic for automatic updates when all tasks are completed etc.
        public bool UpdateProjectStatus(int projectID, ProjectStatus status)
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
                _loggerService.LogAction($"Project status updated ID: {projectID} : {project.Name} :updated to {status}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
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
                _loggerService.LogAction($"Project deleted: ID: {project.ProjectID} : {project.Name}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
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
        public IReadOnlyList<Project> GetManagerProjects(string username)
        {
            try
            {                
                //Filter projects by manager                
                return _projects.Where(p => p.Manager.Equals(username, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Get User Projects: {ex.Message}");
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
