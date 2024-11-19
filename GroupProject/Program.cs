using System.Text.Json;

class Program()
{
    // load users, deserialize user data
    private static async Task<List<JsonElement>> LoadUsersAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("User file not found!");
            return new List<JsonElement>();
        }

        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<JsonElement>>(json) ?? new List<JsonElement>();
    }

    // login method
    private static async Task<User?> LoginAsync(string userFilePath)
    {
        var users = await LoadUsersAsync(userFilePath);

        Console.Write("Enter Email: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        // Find the user element by email and password
        var user = users.FirstOrDefault(u =>
            u.TryGetProperty("Email", out var emailProp) &&
            emailProp.GetString()?.Equals(email, StringComparison.OrdinalIgnoreCase) == true &&
            u.TryGetProperty("Password", out var passwordProp) &&
            passwordProp.GetString() == password);

        if (user.ValueKind == JsonValueKind.Undefined)
        {
            Console.WriteLine("Invalid email or password.");
            return null;
        }

        // get the role
        if (!user.TryGetProperty("Role", out var userRole))
        {
            // handle if no role found in json data
            Console.WriteLine("Role not found for the user.");
            return null;
        }

        string role = userRole.GetString() ?? "Unknown";

        if (role == "Manager")
        {
            // get department for manager
            if (user.TryGetProperty("Department", out var userDepartment))
            {
                string department = userDepartment.GetString() ?? "Unknown";
                // return Manager instance
                return new Manager(
                    user.GetProperty("FirstName").GetString()!,
                    user.GetProperty("LastName").GetString()!,
                    user.GetProperty("EmployeeId").GetInt32(),
                    user.GetProperty("Email").GetString()!,
                    user.GetProperty("Password").GetString()!,
                    role,
                    department
                );
            }
            else
            {
                // if deparment missing from json file
                Console.WriteLine("Manager department not found.");
                return null;
            }
        }

        // TODO (other roles in similar way)
        Console.WriteLine($"Unknown role: {role}");
        return null;
    }



    // main menu
    private static void DisplayMainMenu()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("1 - Login");
        Console.WriteLine("2 - Quit");
    }

    // main
    public static async System.Threading.Tasks.Task Main(string[] args)
    {
        string userFilePath = Path.Combine(Directory.GetCurrentDirectory(), "users.json");
        string tasksFilePath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");

        // run loop while return or braek
        while (true)
        {
            DisplayMainMenu(); // show main menu
            string choice = Console.ReadLine() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    User? user = await LoginAsync(userFilePath);
                    if (user != null)
                    {
                        switch (user.Role)
                        {
                            case "Manager":
                                var managerService = new ManagerService((Manager)user, tasksFilePath);
                                managerService.HandleManagerOptions();
                                break;
                            // TODO (other user type services goes here)
                            default:
                                Console.WriteLine($"Unknown role: {user.Role}");
                                break;
                        }
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
