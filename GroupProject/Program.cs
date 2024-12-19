using System;
using Spectre.Console;
using Spectre.Console.Cli;

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
            IRepository<Log> logRepository = new DataRepository<Log>("logs.json");
            //Create instances of services, inject dependencies
            IPasswordService passwordService = new PasswordService();
            IAuthService authService = new AuthService(passwordService);
            ILoggerService loggerService = new LoggerService(logRepository, authService);
            IUsersService usersService = new UsersService(userRepository, passwordService, loggerService);
            //Inject users service into auth service, cannot be done in constructor as it needs to be initialized first
            authService.SetUsersService(usersService);
            IProjectsService projectsService = new ProjectsService(projectRepository, loggerService);
            ITasksService tasksService = new TasksService(taskRepository, authService, loggerService);
            IServicesUI servicesUI = new ServicesUI(usersService, projectsService, tasksService, loggerService, authService);

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
                    /*
                     * replacing following code with code calling MenuFactory
                    DisplayMenu(authService.CurrentUserRole);
                    int choice = InputService.ReadValidInt("\n  Enter choice: ");
                    ManageMenuChoice(choice, authService.CurrentUserRole, servicesUI);
                    */

                    Menu menu = MenuFactory.CeateMenu(authService.CurrentUserRole);
                    menu.DisplayMenu();
                    int choice = InputService.ReadValidInt("\n Enter choice:");
                    menu.HandleMenuChoice(choice, servicesUI);

                }
            }
        }

        private static void DisplayWelcomeMenu()
        {
            Console.WriteLine("  [1] Login");
            Console.WriteLine("  [2] Exit");
        }

    }
}
