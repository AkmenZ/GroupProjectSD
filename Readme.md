Project Structure
The project is organized into the following main components:
1.	Models: Defines the data structures used in the application.
2.	Services: Contains the business logic and operations related to different entities.
3.	Repositories: Handles data persistence.
4.	Interfaces: Defines contracts for certain behaviors.
5.	Enums: Defines enumerations used throughout the application.
6.	UI: Responsible for handling user interactions and providing user interface.
7.	Program: The entry point of the application.

Models
- User.cs: Abstract base class for all user types. Methods for setting and verifying passwords are also included.
- Administrator.cs: Inherits from User. Represents an administrator user.
- Manager.cs: Inherits from User. Represents a manager user.
- TeamMember.cs: Inherits from User. Represents a team member user with additional properties: FirstName, LastName, Email, and Phone.
- Project.cs: Represents a project with properties: ProjectID, Name, Manager, Status, and Description. Methods for updating project status.
- Task.cs: Represents a task with properties: TaskID, Title, Description, AssignedTo, Status, and ProjectID. Implements the ITrackable interface with methods for starting, updating status, assigning users, and completing tasks.

Services
- AuthService.cs: Manages user authentication. Contains methods for logging in and out, and properties for tracking the current user and his role.
- UsersService.cs: Manages user-related operations: adding, deleting users, changing passwords, and retrieving users by role or username.
- ProjectsService.cs: Manages project-related operations: adding, updating, deleting projects, and retrieving projects by ID or manager.
- TasksService.cs: Manages task-related operations: adding, assigning, starting, stopping, updating, and deleting tasks, and retrieving tasks by ID or user.
- InputService.cs: Provides methods for reading and validating user input from the console.

Repositories
- DataRepository.cs: Handles data persistence using JSON serialization. Contains methods for retrieving and saving data to a file.

Interfaces
- ITrackable.cs: Defines methods for starting, completing, and updating the status of a task.

Enums
- Enums.cs: Defines enumerations for UserRole, ProjectStatus, and TaskStatus.

Program
- Program.cs: The entry point of the application. Contains methods for displaying menus and managing user choices.

Dependencies and Interactions
- AuthService depends on UsersService for user authentication.
- ProjectsService, TasksService, UsersService depend on :
	- DataRepository for data persistence.
	- LoggerService for logging action
	- AuthService for role based access
	- InputService as static class is used for reading user input in the console

Adding New Features
To add a new feature, follow these steps:
1.	Define Model: If the feature requires new data structures, define in the Models folder.
2.	Create Services: Implement the business logic for the new feature in a new or existing service in the Services folder.
3.	Create instance of <T>DataRepository wehre T: class: If the feature requires data persistence.
4.	Inject DataRepository, LoggerService, AuthService to FeatureService through contructor.
6.  	Update ServicesUI to add user interaction logic.
4.	Modify Program: Update the Program.cs to include new menu options and handle user interactions for the new feature.

Modifying Existing Features
To modify existing features:
1.	Identify the Relevant Components: Locate the models, services, and repositories related to the feature.
2.	Update Business Logic: Modify the methods in the relevant service classes.
3.	Update Data Structures: If necessary, update the models to include new properties or methods.
4.	Update UI to include interface handling user interaction. 
5.	Update the Program.cs to reflect changes in menu options.

Backlog:

	// Means done
	/? Partially done / needs review / or followed in future code
	?  Should we do it?
- //JSON Generic repository DataRepository.cs: List<T>GetAll(), List<T>SaveAll()//
- //Bug: Deserialization of properties unique to subclasses
- //LoggerService: LogAction(), GetLogs()
- //InputService: ReadValidString(), RadValidInt(), ReadValidDouble(), ReadValidDate()
- //Authorisation service: CurrentUsername, CurrentUserRole, IsUserLoggedIn, Login(), Logout()
- //PasswordSerivce: GenerateSalt(), HashPassword(), VerifyPassword()
- //Models: Enumms, User, Administrator, Manager, TeamMember, Project, Task
- //Enums: UserRole, ProjectStatus, TaskStatus
- //UserService: AddUser(), DeleteUser(), ChangePassword(), Query methods
- //TasksService: GenerateTaskID(), AddTask(), AssignUserToTask(), StartTask(), StopTask(), //TasksService: UpdateTaskStatus(), DeleteTask(), GetTaskByID(), List Queries
- //ProjectsService: AddProject(), UpdateProjectStatus(), DeleteProject(), GetProjectByID(), //ProjectsService: ListQueries
- //Role based menu: DisplayMenu(UserRole), ManageMenuChoice(choice, UserRole, ServicesUI)
- //Update project status
- //Task ID should be unique for each project
- //start task
- //complete task
- //Test starting task assigned to different user 
- //Task can be added with assignee value 'none'
- //Display project ID when listing tasks
- //ITaskable
- //Seaparating UI from Service - remove write lines statements from services
- //test if task update status persists
- //adding project with "none" as manager - set to string 'none' instead null
- ?Refactor code to use Interfaces
- Create unit tests
- ?Refactor app to use async
- ?Split ServicesUI into separate classes for each service
- Assign Manager to project with 'none'
- Start project
- Complete project
- Task priorities
- Projects timeline (start dates, end dates etc)
- Task timeline (start date, transition date)
- Cascading delete:
-	Deleting user (Assigned tasks? set to none)
-	Deleting project (Project tasks? delete/archive)
- Review user types Intern / QA
- MinimumWIP
- Reporting - calculate Project progress
- Automatic project updates
- Log incorrect logins
- Parse enums input
- Consider archiving
- Get users by role - add to menu
- /?Separation of concern UI - Services (No WriteLine statements in Services)
- /?check object existence when passing as parameter, query object before passing
/?Exception handling
