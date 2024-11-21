Project Structure
The project is organized into the following main components:
1.	Models: Defines the data structures used in the application.
2.	Services: Contains the business logic and operations related to different entities.
3.	Repositories: Handles data persistence.
4.	Interfaces: Defines contracts for certain behaviors.
5.	Enums: Defines enumerations used throughout the application.
6.  UI: Responsible for handling user interactions and providing user interface.
7.	Program: The entry point of the application.

Models
•	User.cs: Abstract base class for all user types. Methods for setting and verifying passwords are also included.
•	Administrator.cs: Inherits from User. Represents an administrator user.
•	Manager.cs: Inherits from User. Represents a manager user.
•	TeamMember.cs: Inherits from User. Represents a team member user with additional properties: FirstName, LastName, Email, and Phone.
•	Project.cs: Represents a project with properties: ProjectID, Name, Manager, Status, and Description. Methods for updating project status.
•	Task.cs: Represents a task with properties: TaskID, Title, Description, AssignedTo, Status, and ProjectID. Implements the ITrackable interface with methods for starting, updating status, assigning users, and completing tasks.

Services
•	AuthService.cs: Manages user authentication. Contains methods for logging in and out, and properties for tracking the current user and his role.
•	UsersService.cs: Manages user-related operations: adding, deleting users, changing passwords, and retrieving users by role or username.
•	ProjectsService.cs: Manages project-related operations: adding, updating, deleting projects, and retrieving projects by ID or manager.
•	TasksService.cs: Manages task-related operations: adding, assigning, starting, stopping, updating, and deleting tasks, and retrieving tasks by ID or user.
•	InputService.cs: Provides methods for reading and validating user input from the console.

Repositories
•	DataRepository.cs: Handles data persistence using JSON serialization. Contains methods for retrieving and saving data to a file.

Interfaces
•	ITrackable.cs: Defines methods for starting, completing, and updating the status of a task.

Enums
•	Enums.cs: Defines enumerations for UserRole, ProjectStatus, and TaskStatus.

Program
•	Program.cs: The entry point of the application. Contains methods for displaying menus and managing user choices.

Dependencies and Interactions
•	AuthService depends on UsersService for user authentication.
•	ProjectsService, TasksService, UsersService depend on :
	- DataRepository for data persistence.
	- LoggerService for logging action
	- AuthService for role based access
•	InputService as static class is used for reading user input in the console


==========================

Adding New Features
To add a new feature, follow these steps:
1.	Define Model: If the feature requires new data structures, define in the Models folder.
2.	Create Services: Implement the business logic for the new feature in a new or existing service in the Services folder.
3.	Create instance of <T>DataRepository wehre T: class: If the feature requires data persistence.
4.  Inject DataRepository, LoggerService, AuthService to FeatureService through contructor
6.  Update ServicesUI.cs to add user interaction logic.
4.	Modify Program: Update the Program.cs to include new menu options and handle user interactions for the new feature.

Modifying Existing Features
To modify existing features:
1.	Identify the Relevant Components: Locate the models, services, and repositories related to the feature.
2.	Update Business Logic: Modify the methods in the relevant service classes.
3.	Update Data Structures: If necessary, update the models to include new properties or methods.
4.  Update UI to include interface handling user interaction. 
5.	Update the Program.cs to reflect changes in menu options.
