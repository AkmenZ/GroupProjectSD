using System.Text.Json;

class Program
{
    // main menu
    private static void DisplayMainMenu()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("1 - Login");
        Console.WriteLine("2 - Quit");
    }

    // main
    public static void Main(string[] args)
    {
        // define data file paths
        string userFilePath = Path.Combine(Directory.GetCurrentDirectory(), "users.json");
        string tasksFilePath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");

        // inject services
        TaskService taskService = new TaskService(tasksFilePath); // instance of task service
        AuthService authService = new AuthService(userFilePath, taskService); // instanciate auth service

        // run loop while return or braek
        while (true)
        {
            DisplayMainMenu(); // show main menu
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    User? user = authService.Login();
                    if (user != null)
                    {
                        user.HandleMenu();
                    }
                    break;
                case "2":
                    Console.WriteLine("Bye bye!"); // close programm
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again!");
                    break;
            }
        }
    }
}
