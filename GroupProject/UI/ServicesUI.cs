using System;
using System.Linq;
using Spectre.Console;

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
                        {
                            user = new Administrator(username, password);
                            break;
                        }
                    case UserRole.Manager:
                        {
                            user = new Manager(username, password);
                            break;
                        }
                    case UserRole.TeamMember:
                        {
                            string firstName = InputService.ReadValidString("\n  Enter first name: ");
                            string lastName = InputService.ReadValidString("\n  Enter last name: ");
                            string email = InputService.ReadValidString("\n  Enter email: ");
                            string phone = InputService.ReadValidString("\n  Enter phone: ");
                            user = new TeamMember(username, password, firstName, lastName, email, phone);
                            break;
                        }
                    case UserRole.Intern:
                        {
                            string firstName = InputService.ReadValidString("\n  Enter first name: ");
                            string lastName = InputService.ReadValidString("\n  Enter last name: ");
                            string mentorUsername = InputService.ReadValidString("\n  Enter mentor username: ");
                            user = new Intern(username, password, firstName, lastName, mentorUsername);
                            break;
                        }
                    default:
                        throw new ArgumentException("\n  Invalid user role");
                }

                //Add user
                if (_usersService.AddUser(user, password))
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

        //Add team member to project
        public void AddTeamMemberToProject()
        {
            try
            {
                //Get project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Get project by ID
                var project = _projectsService.GetProjectById(projectId);
                //Get team member username
                string teamMemberUsername = InputService.ReadValidString("\n  Enter team member's username: ");
                //Verify if team member exists
                var teamMember = _usersService.GetUserByUsername(teamMemberUsername);
                //Add team member to project
                if (_projectsService.AddTeamMember(projectId, teamMember.Username))
                {
                    Console.WriteLine($"\n  Team member: {teamMember.Username} added to project: {project.Name}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Update Project status
        public void UpdateProjectStatus()
        {
            try
            {
                //Get task ID
                int projectId = InputService.ReadValidInt("\n  Enter Project ID: ");
                //Select project status
                ProjectStatus status = SelectProjectStatus();
                //Update project status
                if (_projectsService.UpdateStatus(projectId, status))
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
                //Select task type
                TaskType taskType = SelectTaskType();
                //Read task title
                string taskTitle = InputService.ReadValidString("\n  Enter task title: ");
                //Read task description
                string taskDescription = InputService.ReadValidString("\n  Enter task description: ");
                //Display task priority                
                Console.WriteLine("\n  Select task priority:");
                Console.WriteLine("  [1] Highest");
                Console.WriteLine("  [2] High");
                Console.WriteLine("  [3] Medium");
                Console.WriteLine("  [4] Low");
                Console.WriteLine("  [5] Lowest");                
                //Get task priority choice
                int priorityChoice = InputService.ReadValidInt("\n  Enter choice: ");
                //Convert choice to TaskPriority enum
                TaskPriority priority = (TaskPriority)(priorityChoice - 1);
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
                //Read estimated story points
                double? estimatedStoryPoints = InputService.ReadValidDouble("\n  Enter estimated story points or leave blank: ");
                //Read due date
                DateTime? dueDate = InputService.ReadValidDateTime("\n  Enter due date or leave blank: ");
                //initialize properties unique to respective task type
                string stepsToReproduce = null;
                string reportedBy = null;
                string acceptanceCriteria = null;
                string relatedItems = null;
                string audience = null;
                string author = null;

                //Read specific properties based on task type
                switch (taskType)
                {
                    case TaskType.Bug:
                        stepsToReproduce = InputService.ReadValidString("\n  Enter steps to reproduce: ");
                        break;
                    case TaskType.Feature:
                        acceptanceCriteria = InputService.ReadValidString("\n  Enter acceptance criteria: ");
                        break;
                    case TaskType.Improvement:
                        relatedItems = InputService.ReadValidString("\n  Enter related items: ");
                        break;
                    case TaskType.Documentation:
                        relatedItems = InputService.ReadValidString("\n  Enter related items: ");
                        audience = InputService.ReadValidString("\n  Enter intended audience: ");
                        break;
                }

                // Add task
                if (_tasksService.AddTask(taskType, taskTitle, taskDescription, priority, assignee, project.ProjectID, estimatedStoryPoints, dueDate, stepsToReproduce, acceptanceCriteria, relatedItems, audience))
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

        //Assign task to epic
        public void AssignTaskToEpic()
        {
            try
            {
                //Get task ID
                string taskId = InputService.ReadValidString("\n  Enter task ID: ");
                //Get epic ID
                string epicId = InputService.ReadValidString("\n  Enter epic ID: ");
                //Assign task to epic
                if (_tasksService.AssignTaskToEpic(taskId, epicId))
                {
                    Console.WriteLine("\n  Task assigned to epic successfully.");
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
                if (_tasksService.StartTask(taskId))
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
                if (_tasksService.CompleteTask(taskId))
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
                //Select task status
                TaskStatus status = SelectTaskStatus();
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
                Console.WriteLine($"\n  {_authService.CurrentUsername} Tasks:");
                PrintTasks(tasks);
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
            //Display header
            Console.WriteLine("\n  All Tasks in the system:");
            //Display tasks
            PrintTasks(tasks);
        }

        //List tasks by manager
        public void ListTasksByManager()
        {
            try
            {
                // Get manager username
                string manager = InputService.ReadValidString("\n  Enter manager username: ");
                // Get manager's projects
                var managersProjects = _projectsService.GetProjectsByManager(manager).Select(p => p.ProjectID).ToList();
                // Get tasks by manager's projects
                foreach (var projectID in managersProjects)
                {
                    Console.WriteLine(projectID);
                    var tasks = _tasksService.GetProjectTasks(projectID);
                    // Display header
                    Console.WriteLine($"\n  Tasks managed by {manager} in project: {projectID}:");
                    // Display tasks
                    if (tasks.Any())
                    {
                        PrintTasks(tasks);
                    }
                    else
                    {
                        Console.WriteLine("\n  No tasks found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List project tasks
        public void ListProjectTasks()
        {
            try
            {
                //Get project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Get project tasks
                var tasks = _tasksService.GetProjectTasks(projectId);
                //Display tasks
                Console.WriteLine($"\n  Project ID: {projectId} Tasks:");
                PrintTasks(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List project tasks by status
        public void ListProjectTasksByStatus()
        {
            try
            {
                //Get project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Select task status
                TaskStatus status = SelectTaskStatus();
                //Get project tasks of status
                var tasks = _tasksService.GetProjectTasksByStatus(projectId, status);
                //Display tasks
                PrintTasks(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List epic tasks
        public void ListEpicTasks()
        {
            try
            {
                //Get epic ID
                string epicId = InputService.ReadValidString("\n  Enter epic ID: ");
                //Get epic tasks
                var tasks = _tasksService.GetEpicTasks(epicId);
                //Display header
                Console.WriteLine($"\n  Epic ID: {epicId} tasks:");
                //Display tasks
                PrintTasks(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List Project tasks of type
        public void ListProjectTasksByType()
        {
            try
            {
                //Get project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Select task type
                TaskType taskType = SelectTaskType();
                //Get project tasks of type
                var tasks = _tasksService.GetProjectTasksByType(projectId, taskType);
                //Display header
                Console.WriteLine($"\n  Project ID: {projectId} tasks of type: {taskType}:");
                PrintTasks(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List projects tasks by user
        public void ListProjectTasksByUser()
        {
            try
            {
                //Get project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Get user username
                string username = InputService.ReadValidString("\n  Enter username: ");
                //Get project tasks by user
                var tasks = _tasksService.GetProjectTasksByUser(projectId, username);
                //Display header
                Console.WriteLine($"\n  Project ID: {projectId} tasks assigned to {username}:");
                PrintTasks(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List all projects
        public void ListAllProjects()
        {
            try
            {
                //Get all projects
                var projects = _projectsService.GetAllProjects();
                //Display header
                Console.WriteLine("\n  Projects:");
                //Print projects
                PrintProjects(projects);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List projects by user
        public void ListProjectsByUser()
        {
            Console.Clear();
            try
            {
                //Get user username
                string username = InputService.ReadValidString("\n  Enter username: ");
                //Get projects by user
                var projects = _projectsService.GetProjectsByUser(username);
                //Display header
                Console.WriteLine($"\n  Projects assigned to {username}:");
                //Display projects
                PrintProjects(projects);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ListProjectsByManager()
        {
            try
            {
                //Get manager username
                string manager = InputService.ReadValidString("\n  Enter manager username: ");
                //Get projects by manager
                var projects = _projectsService.GetProjectsByManager(manager);
                //Display header
                Console.WriteLine($"\n  Projects managed by {manager}:");
                //Display projects
                PrintProjects(projects);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List projects by status
        public void ListProjectsByStatus()
        {
            try
            {
                //Select project status
                ProjectStatus status = SelectProjectStatus();
                //Get projects by status
                var projects = _projectsService.GetProjectsByStatus(status);
                //Display header
                Console.WriteLine($"\n  Projects with status: {status}:");
                //Display projects
                PrintProjects(projects);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //List project team members
        public void ListProjectTeamMembers()
        {
            try
            {
                //Get project ID
                int projectId = InputService.ReadValidInt("\n  Enter project ID: ");
                //Get project team members
                var teamMembers = _projectsService.GetProjectTeamMemebrs(projectId);
                //Display header
                Console.WriteLine($"\n  Project ID: {projectId} Team members:\n");
                //Display team members
                foreach (var member in teamMembers)
                {
                    Console.WriteLine($"  {member}");
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
                if (_authService.CurrentUserRole != UserRole.Administrator)
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

        //Selecet project status
        public ProjectStatus SelectProjectStatus()
        {
            //Display project statuses
            Console.WriteLine("\n  Select project status:");
            Console.WriteLine("  [1] Created");
            Console.WriteLine("  [2] In Progress");
            Console.WriteLine("  [3] Completed");
            Console.WriteLine("  [4] On Hold");
            //Get project status choice
            int projectStatusChoice = InputService.ReadValidInt("\n  Enter choice: ");
            //Convert choice to ProjectStatus enum
            ProjectStatus projectStatus = (ProjectStatus)(projectStatusChoice - 1);
            return projectStatus;
        }

        //Select task type
        public TaskType SelectTaskType()
        {
            //Display task types
            Console.WriteLine("\n  Select task type:");
            Console.WriteLine("  [1] Epic");
            Console.WriteLine("  [2] Idea");
            Console.WriteLine("  [3] Bug");
            Console.WriteLine("  [4] Feature");
            Console.WriteLine("  [5] Improvement");
            Console.WriteLine("  [6] Documentation");
            //Get task type choice
            int taskTypeChoice = InputService.ReadValidInt("\n  Enter choice: ");
            //Convert choice to TaskType enum
            TaskType taskType = (TaskType)(taskTypeChoice - 1);
            return taskType;
        }

        //Select task status
        public TaskStatus SelectTaskStatus()
        {
            //Display task statuses
            Console.WriteLine("\n  Select task status:");
            Console.WriteLine("  [1] Backlog");
            Console.WriteLine("  [2] ToDo");
            Console.WriteLine("  [3] InProgress");
            Console.WriteLine("  [4] Completed");
            Console.WriteLine("  [5] Blocked");
            //Get task status choice
            int taskStatusChoice = InputService.ReadValidInt("\n  Enter choice: ");
            //Convert choice to TaskStatus enum
            TaskStatus taskStatus = (TaskStatus)(taskStatusChoice - 1);
            return taskStatus;
        }

        //Print tasks
        public void PrintTasks(IReadOnlyList<Task> tasks)
        {
            Console.WriteLine("");
            foreach (var task in tasks)
            {
                Console.WriteLine($"\t  {task.Title}");
                Console.WriteLine($"\t  {task.Type,-14} Assigned To: {task.AssignedTo}");
                Console.WriteLine($"\t  Task ID: {task.TaskID,-5} {(task.Type != TaskType.Epic && !string.IsNullOrEmpty(task.EpicID) ? $"Part of Epic: {task.EpicID} " : ""),-20} Project  ID: {task.ProjectID,-5} Status: {task.Status, -10}  Priority: {task.Priority}");
                Console.WriteLine($"\t  {task.Description}\n");
            }
        }

        public void PrintProjects(IReadOnlyList<Project> projects)
        {
            Console.WriteLine("\n");
            foreach (var project in projects)
            {
                Console.WriteLine($"\tID: {project.ProjectID,-5} Name: {project.Name,-40} Status: {project.Status,-20} Manager: {project.Manager,-15}");
                Console.WriteLine($"\t          Team Members: {string.Join(", ", project.TeamMembers)}");
                Console.WriteLine($"\t          Description: {project.Description}\n");
            }
        }

        public void ViewLogs()
        {
            var logs = _loggerService.GetLogs();
            foreach (var log in logs)
            {
                Console.WriteLine($"  {log.Time} {log.LogID}, Item ID: {log.ItemID}, Item: {log.Item} : {log.Action}, By: {log.Username} : {log.Role}");
            }
        }
    }
}
