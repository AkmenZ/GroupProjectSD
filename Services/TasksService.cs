using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApp
{
    public class TasksService
    {
        //Private list of tasks
        private List<Task> _tasks = new();              
        //Property to store task repository
        private readonly DataRepository<Task> _taskRepository;
        //Property to store project repository
        private LoggerService _loggerService;
        private AuthService _authService;

        //Constructor, dependency injection
        public TasksService(DataRepository<Task> taskRepository, LoggerService loggerService, AuthService authService)
        {
            _taskRepository = taskRepository;            
            _loggerService = loggerService;
            _authService = authService;
            _tasks = _taskRepository.GetAll();         
        }

        //Generate task ID
        private string GenerateTaskID(int nextTaskID, int projectID)
        {
            return $"{nextTaskID}.{projectID}";
        }

        //Add task to project
        public bool AddTask(string title, string description, string assignedTo, int projectID)
        {
            //Get task data
            _tasks = _taskRepository.GetAll();
            // Determine the next task ID for the project
            int nextTaskID = _tasks.Where(t => t.ProjectID == projectID)
                                   .Select(t => int.Parse(t.TaskID.Split('.')[0])).DefaultIfEmpty(0).Max() + 1;
            //Create new task
            var task = new Task(GenerateTaskID(nextTaskID, projectID), title, description, assignedTo, projectID);
            //Add task to tasks list
            _tasks.Add(task);
            //Save changes to tasks
            _taskRepository.SaveAll(_tasks);           
            //Log action
            _loggerService.LogAction($"Task assigned project Task ID: {task.TaskID}: {title} : project ID:{projectID}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
            //Return true when task added
            return true;
        }

        //Assign user to a task
        public bool AssignUserToTask(string taskId, string username)
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
            _loggerService.LogAction($"Task assigned user: {username} : Task ID: {taskId} : {task.Title}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
            //Return true when user assigned            
            return true;
        }

        //Start task
        public bool StartTask(string taskId)
        {
            //Get data
            _tasks = _taskRepository.GetAll();
            //Find task by ID
            var task = GetTaskById(taskId);
            //Start task
            if (task.StartTask(_authService.CurrentUsername))
            {
                return true;
            }
            //Save changes
            _taskRepository.SaveAll(_tasks);
            //Log action
            _loggerService.LogAction($"Task started ID: {taskId} : {task.Title}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
            //Return true when task started
            return false;
        }

        //Complete task
        public bool StopTask(string taskId) 
        {
            //Get data
            _tasks = _taskRepository.GetAll();
            //Find task by ID
            var task = GetTaskById(taskId);
            //Complete task
            task.CompleteTask(_authService.CurrentUsername);
            //Save changes
            _taskRepository.SaveAll(_tasks);
            //Log action
            _loggerService.LogAction($"Task completed ID: {taskId} : {task.Title}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
            //Return true when task completed
            return true;
        }

        //Update task status
        public bool UpdateTaskStatus(string taskId, TaskStatus status)
        {
            //Get data
            _tasks = _taskRepository.GetAll();
            //Find task by ID
            var task = GetTaskById(taskId);            
            //Update task status
            task.UpdateStatus(status);
            //Save changes
            _taskRepository.SaveAll(_tasks);
            //Log action
            _loggerService.LogAction($"Task status updated ID: {taskId} : {task.Title} : updated to : {status}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
            //Return true when status updated
            return true;
        }

        //Delete task
        public bool DeleteTask(string taskId)
        {
            //Get data
            _tasks = _taskRepository.GetAll();
            //Find task by ID
            var task = GetTaskById(taskId);            
            //Delete task
            _tasks.Remove(task);
            //Save changes
            _taskRepository.SaveAll(_tasks);
            //Log action
            _loggerService.LogAction($"Task deleted ID: {taskId} : {task.Title}", _authService.CurrentUserRole.ToString(), _authService.CurrentUsername);
            //Return true when task deleted
            return true;
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

        //Get user taskss
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