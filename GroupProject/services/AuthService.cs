using System.Text.Json;

public class AuthService
{
    // props
    private readonly string UserFilePath;
    private readonly TaskService TaskService;

    // constrictor
    public AuthService(string userFilePath, TaskService taskService)
    {
        UserFilePath = userFilePath;
        TaskService = taskService;
    }

    // methods
    // load users, deserialize user data
    private List<JsonElement> LoadUsers()
    {
        if (!File.Exists(UserFilePath))
        {
            Console.WriteLine("User file not found!");
            return new List<JsonElement>();
        }

        string json = File.ReadAllText(UserFilePath);
        return JsonSerializer.Deserialize<List<JsonElement>>(json) ?? new List<JsonElement>();
    }

    // login and instanciate user object
    public User? Login()
    {
        var users = LoadUsers();

        Console.Write("Enter Email: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter Password: ");
        string password = Console.ReadLine() ?? string.Empty;

        // get user by email and password
        var user = users.FirstOrDefault(u =>
            u.TryGetProperty("Email", out var emailProp) &&
            emailProp.GetString()?.Equals(email, StringComparison.OrdinalIgnoreCase) == true &&
            u.TryGetProperty("Password", out var passwordProp) &&
            passwordProp.GetString() == password);

        if (user.ValueKind == JsonValueKind.Undefined)
        {
            Console.WriteLine("Invalid email or password!");
            return null;
        }

        // get user role
        if (!user.TryGetProperty("Role", out var userRole))
        {
            Console.WriteLine("Role not found for the user!");
            return null;
        }

        string role = userRole.GetString() ?? "Unknown";

        // instanciate users based on role
        switch (role)
        {
            case "Manager":
                return CreateManager(user);

            case "Intern":
                return CreateIntern(user);

            default:
                Console.WriteLine($"Unknown role: {role}");
                return null;
        }
    }

    public List<JsonElement> GetNonManagerUsers()
    {
        var users = LoadUsers();
        var nonManagerUsers = new List<JsonElement>();

        foreach (var user in users)
        {
            // add to list users who are not managers
            if (user.TryGetProperty("Role", out var userRole) && userRole.GetString() != "Manager")
            {
                nonManagerUsers.Add(user);
            }
        }
        return nonManagerUsers;
    }

    // create manager user object
    private Manager? CreateManager(JsonElement user)
    {
        if (user.TryGetProperty("Department", out var userDepartment))
        {
            string department = userDepartment.GetString() ?? "Unknown";
            return new Manager(
                user.GetProperty("FirstName").GetString()!,
                user.GetProperty("LastName").GetString()!,
                user.GetProperty("EmployeeId").GetInt32(),
                user.GetProperty("Email").GetString()!,
                user.GetProperty("Password").GetString()!,
                "Manager",
                department,
                TaskService,
                this // this means the AuthService itself, since manager will use it to assign users to tasks
            );
        }

        Console.WriteLine("Manager department not found!");
        return null;
    }

    // create intern user object
    private Intern? CreateIntern(JsonElement user)
    {
        return new Intern(
            user.GetProperty("FirstName").GetString()!,
            user.GetProperty("LastName").GetString()!,
            user.GetProperty("EmployeeId").GetInt32(),
            user.GetProperty("Email").GetString()!,
            user.GetProperty("Password").GetString()!,
            "Intern",
            TaskService
        );
    }
}
