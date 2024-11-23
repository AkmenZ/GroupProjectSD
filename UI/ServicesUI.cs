using System;
using System.Linq;

namespace ProjectManagementApp
{
    public class ServicesUI : IServicesUI
    {
        //Properties to hold dependencies
        private readonly IUsersService _usersService;
        private readonly IProjectsService _projectsService;
        private readonly ITasksService _tasksService;
        private readonly IAuthService _authService;
        private readonly ILoggerService _loggerService;

        //Constructor, dependency injection
        public ServicesUI(IUsersService usersService, IProjectsService projectsService, ITasksService tasksService, ILoggerService loggerService, IAuthService authService)
        {
            _usersService = usersService;
            _projectsService = projectsService;
            _tasksService = tasksService;
            _authService = authService;
            _loggerService = loggerService;
        }

        //Login
        public void Login()
        {
            while (true)
            {
                string username = InputService.ReadValidString("\n  Enter username: ");
                string password = InputService.ReadLineMasked();

                try
                {
                    if (_authService.Login(username, password))
                    {
                        Console.WriteLine("\n  Login successful.");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n  {ex.Message}");
                }
            }
        }

        //Logout from current session
        public void Logout()
        {
            _authService.Logout();
            Console.WriteLine("\n  You have been logged out.");
        }

        public void CreateUser(UserRole role)
        {
            try
            {
                //Get username and password
                string username = InputService.ReadValidString("\n  Enter username: ");
                string password = InputService.ReadLineMasked();

                //Create user based on role
                User user;
                switch (role)
                {
                    case UserRole.Administrator:
                        user = new Administrator(username, password);
                        break;
                    case UserRole.Manager:
                        user = new Manager(username, password);
                        break;
                    case UserRole.TeamMember:
                        string firstName = InputService.ReadValidString("\n  Enter first name: ");
                        string lastName = InputService.ReadValidString("\n  Enter last name: ");
                        string email = InputService.ReadValidString("\n  Enter email: ");
                        string phone = InputService.ReadValidString("\n  Enter phone: ");
                        user = new TeamMember(username, password, firstName, lastName, email, phone);
                        break;
                    default:
                        throw new ArgumentException("\n  Invalid user role");
                }

                //Add user
                if (_usersService.AddUser(user))
                {
                    Console.WriteLine($"\n  {role} account created successfully. Username: {username}");
                    return;
                }
                else
                {
                    Console.WriteLine("\n  Username already exists. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }

        //Delete User
        public void DeleteUser()
        {
            try
            {
                string username = InputService.ReadValidString("\n  Enter username to delete: ");

                //Need to consider logic what happens when a suer has projects or tasks assigned.
                //For now, just delete the user. Should we remove assigmments first?

                if (_usersService.DeleteUser(username))
                {
                    Console.WriteLine($"\n  User: {username} deleted sucessfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List all users
        public void ListAllUsers()
        {
            try
            {
                var users = _usersService.GetAllUsers();
                Console.WriteLine("\n  Users:");
                foreach (var user in users)
                {
                    Console.WriteLine($"  Username: {user.Username}, Role: {user.Role}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Create Project : manager
        public void CreateProject()
        {
            try
            {
                //Get project name
                string projectName = InputService.ReadValidString("\n  Enter project name: ");
                //For now a project manager is assigned to a project at creation.
                //Need to consider business logic and perhaps refactor to allow null manager at creation.
                string managerUsername = InputService.ReadValidString("\n  Enter project manager's username or 'none': ");
                string projectDescription = InputService.ReadValidString("\n  Enter project description: ");
                //Initialize manager with 'none' as default
                string manager = "none";
                //Check if 'none' is entered
                if (managerUsername != "none")
                {
                    //Verify if manager exists, assign username
                    manager = _usersService.GetManagerByUsername(managerUsername).Username.ToString();
                }

                //Add project
                if (_projectsService.AddProject(projectName, manager, projectDescription))
                {
                    Console.WriteLine($"\n  Project '{projectName}' created successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Update task status
        public void UpdateProjectStatus()
        {
            try
            {
                //Get task ID
                int projectId = InputService.ReadValidInt("\n  Enter Project ID: ");
                //Display status options
                Console.WriteLine("\n  Select new status:");
                Console.WriteLine("  [1] Not Started");
                Console.WriteLine("  [2] In Progress");
                Console.WriteLine("  [3] Completed");
                Console.WriteLine("  [4] On Hold");
                //Get status choice
                int statusChoice = InputService.ReadValidInt("\n  Enter choice: ");
                //Convert choice to Project Status enum
                ProjectStatus status = (ProjectStatus)(statusChoice - 1);
                //Update task status
                if (_projectsService.UpdateProjectStatus(projectId, status))
                {
                    Console.WriteLine($"\n  Project: {projectId} status updated to: {status}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Delete Project
        public void DeleteProject()
        {
            try
            {
                //Get project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Delete project
                if (_projectsService.DeleteProject(projectId))
                {
                    Console.WriteLine($"\n  Project ID: {projectId} deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Add task to project
        public void AddTask()
        {
            try
            {
                //TO be decided should Project ID be insterted manually or it could be set by current project, might depend on role.
                //Read project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Get project by ID
                var project = _projectsService.GetProjectById(projectId);
                //Read task title
                string taskTitle = InputService.ReadValidString("\n  Enter task title: ");
                //Read task description
                string taskDescription = InputService.ReadValidString("\n  Enter task description: ");
                //Read assignee ID
                string taskAssigneeUsername = InputService.ReadValidString("\n  Enter username to assign to a task or 'none': ");
                //Check if choice is 'none'
                string assignee = null;
                if (taskAssigneeUsername == "none")
                {
                    assignee = "none";                    
                }
                //Get assignee
                else
                {
                    assignee = _usersService.GetUserByUsername(taskAssigneeUsername).Username.ToString();
                }

                //Add task
                if (_tasksService.AddTask(taskTitle, taskDescription, assignee, project.ProjectID))
                {
                    Console.WriteLine($"\n  Task: {taskTitle} added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Assign user to a task
        public void AssignUserToTask()
        {
            try
            {
                //Get task ID
                string taskId = InputService.ReadValidString("\n  Enter task ID: ");
                //Get task
                var task = _tasksService.GetTaskById(taskId);
                //Get assignee ID
                string assigneeUsername = InputService.ReadValidString("\n  Enter assignee's username: ");
                //Get assignee
                var assignee = _usersService.GetUserByUsername(assigneeUsername);
                //Assign user to task
                if (_tasksService.AssignUserToTask(taskId, assignee.Username))
                {
                    Console.WriteLine("\n  User assigned to task successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Start task
        public void StartTask()
        {
            try
            {
                //Get task ID
                string taskId = InputService.ReadValidString("\n  Enter task ID: ");
                //Start task
                if (_tasksService.StartTask(taskId, _authService.CurrentUsername))
                {
                    Console.WriteLine("\n  Task started successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Complete task
        public void CompleteTask()
        {
            try
            {
                //Get task ID
                string taskId = InputService.ReadValidString("\n  Enter task ID: ");
                //Complete task
                if (_tasksService.StopTask(taskId, _authService.CurrentUsername))
                {
                    Console.WriteLine("\n  Task completed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Update task status
        public void UpdateTaskStatus()
        {
            try
            {
                //Get task ID
                string taskId = InputService.ReadValidString("\n  Enter task ID: ");
                //Display status options
                Console.WriteLine("  Select new status:");
                Console.WriteLine("  [1] ToDo");
                Console.WriteLine("  [2] InProgress");
                Console.WriteLine("  [3] Completed");
                Console.WriteLine("  [4] Blocked");
                //Get status choice
                int statusChoice = InputService.ReadValidInt("\n  Enter choice: ");
                //Convert choice to TaskStatus enum
                TaskStatus status = (TaskStatus)(statusChoice - 1);
                //Update task status
                if (_tasksService.UpdateTaskStatus(taskId, status))
                {
                    Console.WriteLine("\n  Task status updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Delete task
        public void DeleteTask()
        {
            try
            {
                //Get task ID
                string taskId = InputService.ReadValidString("\n  Enter task ID: ");
                //Delete task
                if (_tasksService.DeleteTask(taskId))
                {
                    Console.WriteLine("\n  Task deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List user tasks
        public void ListUserTasks()
        {
            try
            {
                //Get user tasks
                var tasks = _tasksService.GetUserTasks(_authService.CurrentUsername);
                //Display tasks
                Console.WriteLine($"\n  {_authService.CurrentUsername} tasks:");
                foreach (var task in tasks)
                {
                    Console.WriteLine($"  Task ID: {task.TaskID}, Title: {task.Title}, Description: {task.Description}, Status: {task.Status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List all tasks
        public void ListAllTasks()
        {
            //Get all tasks
            var tasks = _tasksService.GetAllTasks();
            //Display tasks
            Console.WriteLine("\n  Tasks:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"  Task  ID: {task.TaskID}, Project ID: {task.ProjectID} Title: {task.Title}, Description: {task.Description}, Status: {task.Status}, Assigned To: {task.AssignedTo}");
            }
        }

        //List all projects
        public void ListAllProjects()
        {
            try
            {
                //Get all projects
                var projects = _projectsService.GetAllProjects();
                //Display projects
                Console.WriteLine("\n  Projects:");
                foreach (var project in projects)
                {
                    Console.WriteLine($"  ID: {project.ProjectID}, Name: {project.Name}, Status: {project.Status}, Manager: {project.Manager}, Description: {project.Description}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        public void ChangePassword()
        {
            try
            {
                string username = null;

                //Change password
                if (_authService.CurrentUserRole == UserRole.TeamMember ||
                    _authService.CurrentUserRole == UserRole.Manager)
                {
                    username = _authService.CurrentUsername;

                }
                else if (_authService.CurrentUserRole == UserRole.Administrator)
                {
                    username = InputService.ReadValidString("\n  Enter username: ");

                }
                else
                {
                    Console.WriteLine("\n  Insufficient rights.");
                }

                //Get new password.
                string newPassword = InputService.ReadLineMasked();

                //Change password
                if (_usersService.ChangePassword(username, newPassword))
                {
                    Console.WriteLine($"\n  Password for user: {username} changed successfully.");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error, change password: {ex.Message}");
            }
        }

        public void ViewLogs()
        {
            var logs = _loggerService.GetLogs();
            foreach (var log in logs)
            {
                Console.WriteLine(log);
            }
        }
    }
}
