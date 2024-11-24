using System;

namespace ProjectManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create instances of repositories
            IRepository<User> userRepository = new DataRepository<User>("users.json");
            IRepository<Project> projectRepository = new DataRepository<Project>("projects.json");
            IRepository<Task> taskRepository = new DataRepository<Task>("tasks.json");
            //Create instances of services, inject dependencies
            IPasswordService passwordService = new PasswordService();
            IAuthService authService = new AuthService(passwordService);
            ILoggerService loggerService = new LoggerService(authService);            
            IUsersService usersService = new UsersService(userRepository, passwordService, loggerService);
            //Inject users service into auth service, cannot be done in constructor as it needs to be initialized first
            authService.SetUsersService(usersService);
            IProjectsService projectsService = new ProjectsService(projectRepository, loggerService);
            ITasksService tasksService = new TasksService(taskRepository, loggerService);
            ServicesUI servicesUI = new(usersService, projectsService, tasksService, loggerService, authService);

            // Set console encoding to UTF-8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Set console encoding to UTF-8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("\t======= Welcome to the Project Management System! =======\n");

            //Check if administrator exists
            try
            {
                if (!usersService.GetAllUsers().Any(u => u.Role == UserRole.Administrator))
                {
                    Console.WriteLine("\n  No administrator account found. Please create one.");
                    servicesUI.CreateUser(UserRole.Administrator);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n  Error checking administrator account: {ex.Message}");
                return;
            }
                        
            while (true)
            {
                if (!authService.IsUserLoggedIn)
                {
                    DisplayWelcomeMenu();
                    int choice = InputService.ReadValidInt("\n  Enter choice: ");
                    switch (choice)
                    {
                        case 1:
                            servicesUI.Login();
                            break;
                        case 2:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("\n  Invalid choice.");
                            break;
                    }
                }
                else
                {
                    DisplayMenu(authService.CurrentUserRole);
                    int choice = InputService.ReadValidInt("\n  Enter choice: ");
                    ManageMenuChoice(choice, authService.CurrentUserRole, servicesUI);
                }
            }
        }

        private static void DisplayWelcomeMenu()
        {            
            Console.WriteLine("  [1] Login");
            Console.WriteLine("  [2] Exit");
        }

        private static void DisplayMenu(UserRole role)
        {
            Console.WriteLine("\n  Menu:");
            if (role == UserRole.Administrator)
            {
                Console.WriteLine("  [1] Create New Team Member");
                Console.WriteLine("  [2] Create New Intern");
                Console.WriteLine("  [3] Create New Manager");
                Console.WriteLine("  [4] Create New Administrator");
                Console.WriteLine("  [5] List All Users");
                Console.WriteLine("  [6] Delete User");
                Console.WriteLine("  [7] Change Password");
                Console.WriteLine("  [8] View Logs");                
                Console.WriteLine("  [0] Logout");
            }
            else if (role == UserRole.Manager)
            {
                Console.WriteLine("  [1]  Create Project");
                Console.WriteLine("  [2]  Update Project Status");
                Console.WriteLine("  [3]  Delete Project");
                Console.WriteLine("  [4]  Add Task to Project");
                Console.WriteLine("  [5]  Assign User Task");
                Console.WriteLine("  [6]  Update Task Status");
                Console.WriteLine("  [7]  Start Task");
                Console.WriteLine("  [8]  Complete Task");
                Console.WriteLine("  [9]  List All Projects");
                Console.WriteLine("  [10] List All Tasks");
                Console.WriteLine("  [11] Change Password");
                Console.WriteLine("  [0]  Logout");
            }
            else if (role == UserRole.TeamMember)
            {                
                Console.WriteLine("  [1] List Assigned Tasks by Project");
                Console.WriteLine("  [2] List All Tasks");
                Console.WriteLine("  [3] Update Task Status");
                Console.WriteLine("  [4] Start Task");
                Console.WriteLine("  [5] Complete Task");
                Console.WriteLine("  [6] Change Password");
                Console.WriteLine("  [0] Logout");
            }
        }

        private static void ManageMenuChoice(int choice, UserRole role, ServicesUI servicesUI)
        {
            if (role == UserRole.Administrator)
            {
                switch (choice)
                {
                    //Perhaps user creation should be done through 1 option with a role selection
                    case 1:
                        servicesUI.CreateUser(UserRole.TeamMember);
                        break;
                    case 2:
                        servicesUI.CreateUser(UserRole.Intern);
                        break;
                    case 3:
                        servicesUI.CreateUser(UserRole.Manager);
                        break;
                    case 4:
                        servicesUI.CreateUser(UserRole.Administrator);
                        break;
                    case 5:
                        servicesUI.ListAllUsers();
                        break;
                    case 6:
                        servicesUI.DeleteUser();
                        break;                    
                    case 7:
                        servicesUI.ChangePassword();
                        break;
                    case 8:
                        servicesUI.ViewLogs();                        
                        break;
                    case 0:
                        servicesUI.Logout();
                        break;
                    default:
                        Console.WriteLine("\n  Invalid choice.");
                        break;
                }
            }
            else if (role == UserRole.Manager)
            {
                switch (choice)
                {
                    case 1:
                        servicesUI.CreateProject();
                        break;
                    case 2:
                        servicesUI.UpdateProjectStatus();
                        break;
                    case 3:
                        servicesUI.DeleteProject();
                        break;
                    case 4:
                        servicesUI.AddTask();
                        break;
                    case 5:
                        servicesUI.AssignUserToTask();
                        break;
                    case 6:
                        servicesUI.UpdateTaskStatus();
                        break;
                    case 7:
                        servicesUI.StartTask();
                        break;
                    case 8:
                        servicesUI.CompleteTask();
                        break;
                    case 9:
                        servicesUI.ListAllProjects();
                        break;
                    case 10:
                        servicesUI.ListAllTasks();
                        break;
                    case 11:
                        servicesUI.ChangePassword();
                        break;
                    case 0:
                        servicesUI.Logout();
                        break;
                    default:
                        Console.WriteLine("\n  Invalid choice.");
                        break;
                }
            }
            else if (role == UserRole.TeamMember)
            {
                switch (choice)
                {   
                    case 1:
                        servicesUI.ListUserTasks();
                        break;
                    case 2:
                        servicesUI.ListAllTasks();
                        break;
                    case 3:
                        servicesUI.UpdateTaskStatus();
                        break;
                    case 4:
                        servicesUI.StartTask();
                        break;
                    case 5:
                        servicesUI.CompleteTask();
                        break;
                    case 6:
                        servicesUI.ChangePassword();
                        break;
                    case 0:
                        servicesUI.Logout();
                        break;
                    default:
                        Console.WriteLine("\n  Invalid choice.");
                        break;
                }
            }
        }
    }
}
