using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp
{
    public class TasksService : ITasksService
    {
        //Private list of tasks
        private List<Task> _tasks;
        //Property to store task repository
        private IRepository<Task> _taskRepository;
        //Property to store authentication service
        private readonly IAuthService _authService;
        //Property to store logger service
        private readonly ILoggerService _loggerService;

        //Constructor, dependency injection
        public TasksService(IRepository<Task> taskRepository, IAuthService authService, ILoggerService loggerService)
        {
            _taskRepository = taskRepository;
            _authService = authService;
            _loggerService = loggerService;
            _tasks = _taskRepository.GetAll();
        }

        //Add task to project
        public bool AddTask(string title, string description, string assignedTo, int projectID)
        {
            try
            {
                //Get task data
                _tasks = _taskRepository.GetAll();
                //Determine the next task ID for the project
                int nextTaskID = _tasks.Where(t => t.ProjectID == projectID)
                                       .Select(t => int.Parse(t.TaskID
                                       .Split('.')[0])).DefaultIfEmpty(0).Max() + 1;
                //Create new task
                var task = new Task(nextTaskID, title, description, assignedTo, projectID);
                //Add task to tasks list
                _tasks.Add(task);
                //Save changes to tasks
                _taskRepository.SaveAll(_tasks);
                //Log action
                _loggerService.LogAction("Task", task.TaskID.ToString(), $"assigned to Project:{projectID}");
                //Return true when task added
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Add Task: {ex.Message}");
            }
        }

        //Assign user to a task
        public bool AssignUserToTask(string taskId, string username)
        {
            try
            {
                //Get data
                _tasks = _taskRepository.GetAll();
                //Find task by ID
                var task = GetTaskById(taskId);
                //Assign user to task
                task.AssignUser(username);
                //Save changes
                _taskRepository.SaveAll(_tasks);
                //Log action
                _loggerService.LogAction("Task", task.TaskID.ToString(), $"user assigned: {username}");
                //Return true when user assigned            
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Assign User to Task: {ex.Message}");
            }
        }

        //Start task
        public bool StartTask(string taskID)
        {
            try
            {
                //Get data
                _tasks = _taskRepository.GetAll();
                //Find task by ID
                var task = GetTaskById(taskID);
                //Verify ownership
                if (!VerifyOwnership(task.TaskID) && task.AssignedTo != "none")
                {
                    throw new Exception("Only assigned user or a manager can start the task.");
                }
                //Start task
                task.StartTask(_authService.CurrentUsername);                    
                //Save changes
                _taskRepository.SaveAll(_tasks);                
                //Log action
                _loggerService.LogAction("Task", task.TaskID.ToString(), "Started");
                //Return true when task started
                return true;                
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Start Task: {ex.Message}");
            }
        }

        //Complete task
        public bool CompleteTask(string taskID)
        {
            try
            {
                //Get data
                _tasks = _taskRepository.GetAll();
                //Find task by ID
                var task = GetTaskById(taskID);
                //Verify ownership
                if (!VerifyOwnership(task.TaskID))
                {
                    throw new Exception("Only assigned user or a manager can complete the task.");
                }
                //Complete task
                task.CompleteTask(_authService.CurrentUsername);
                //Save changes
                _taskRepository.SaveAll(_tasks);
                //Log action
                _loggerService.LogAction("Task", task.TaskID.ToString(), "Completed");
                //Return true when task completed
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Complete Task: {ex.Message}");
            }
        }

        //Update task status
        public bool UpdateTaskStatus(string taskId, TaskStatus status)
        {
            try
            {
                //Get data
                _tasks = _taskRepository.GetAll();
                //Find task by ID
                var task = GetTaskById(taskId);
                //Verify ownership
                if (!VerifyOwnership(task.TaskID))
                {
                    throw new Exception("Only assigned user or a manager can update the task status.");
                }
                //Update task status
                task.UpdateStatus(status);
                //Save changes
                _taskRepository.SaveAll(_tasks);
                //Log action
                _loggerService.LogAction("Task", task.TaskID.ToString(), $"Status update to: {status}");
                //Return true when status updated
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Update Task Status: {ex.Message}");
            }
        }

        //Delete task
        public bool DeleteTask(string taskId)
        {
            try
            {
                //Get data
                _tasks = _taskRepository.GetAll();
                //Find task by ID
                var task = GetTaskById(taskId);
                //Verify ownership
                if (!VerifyOwnership(task.TaskID))
                {
                    throw new Exception("Only assigned user or a manager can delete the task.");
                }
                //Delete task
                _tasks.Remove(task);
                //Save changes
                _taskRepository.SaveAll(_tasks);
                //Log action
                _loggerService.LogAction("Task", task.TaskID.ToString(), "Deleted");
                //Return true when task deleted
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Delete Task: {ex.Message}");
            }
        }

        //Verify access to task
        public bool VerifyOwnership(string taskId)
        {
            try
            {                
                // Find task by ID
                var task = GetTaskById(taskId);
                // Check if the user is assigned to the task or is a manager, or if the task is assigned to "none"
                if (task.AssignedTo == _authService.CurrentUsername ||
                    _authService.CurrentUserRole == UserRole.Manager)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error Verify Ownership: {ex.Message}");
            }
        }

        //Get task by ID
        public Task GetTaskById(string taskId)
        {
            try
            {
                var task = _tasks.FirstOrDefault(t => t.TaskID == taskId);
                if (task == null)
                {
                    throw new Exception($"Task with ID {taskId} not found.");
                }
                
                return task;
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error, get task by ID: {ex.Message}");
            }
        }

        //Get user taskss, return as read-only list
        public IReadOnlyList<Task> GetUserTasks(string username)
        {
            try
            {
                return _tasks.OrderBy(t => t.ProjectID)
                             .ThenBy(t => t.Status)
                             .Where(t => t.AssignedTo.Equals(username, StringComparison.OrdinalIgnoreCase))
                             .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error, get user tasks: {ex.Message}");
            }
        }

        //Get project tasks return as read-only list
        public IReadOnlyList<Task> GetProjectTasks(int projectID)
        {
            try
            {
                return _tasks.OrderBy(t => t.ProjectID)
                             .ThenBy(t => t.Status)
                             .Where(t => t.ProjectID == projectID)
                             .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error, get project tasks: {ex.Message}");
            }
        }

        //Get tasks by status, return as read-only list
        public IReadOnlyList<Task> GetProjectTasksByStatus(int projectID, TaskStatus status)
        {
            try
            {
                return _tasks.OrderBy(t => t.ProjectID)                             
                             .Where(t => t.ProjectID == projectID && t.Status == status)
                             .ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error, get project tasks by status: {ex.Message}");
            }
        }

        //Get all tasks, return as read-only list
        public IReadOnlyList<Task> GetAllTasks()
         {            
            try
            {
                return _tasks.OrderBy(t => t.ProjectID).ThenBy(t => t.Status).ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"\n  Error, get all tasks: {ex.Message}");                
            }
         }
    }
}