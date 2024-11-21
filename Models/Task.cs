using System;
using Newtonsoft.Json;  

namespace ProjectManagementApp
{
    public class Task: ITrackable
    {
        [JsonProperty]
        public int _nextTaskID = 1;
        [JsonProperty]
        public string TaskID { get; private set; }
        [JsonProperty]
        public string Title { get; private set; }
        [JsonProperty]
        public string Description { get; private set; }
        [JsonProperty]
        public string AssignedTo { get; private set; }
        [JsonProperty]
        public TaskStatus Status { get; private set; }
        [JsonProperty]
        public int ProjectID { get; private set; }

        [JsonConstructor]
        public Task(string taskID, string title, string description, string assignedTo, int projectID)
        {
            TaskID = taskID;
            Title = title;
            Description = description;
            AssignedTo = assignedTo;
            Status = TaskStatus.ToDo;
            ProjectID = projectID;
        }
  
        //Start task by a user : Itrackable
        public bool StartTask(string username)
        {
            try
            {
                //Check if task is assigned to user
                if (!AssignedTo.Equals(username, StringComparison.OrdinalIgnoreCase) & AssignedTo != "none")
                {
                    throw new InvalidOperationException("Only the assigned user can start the task.");
                }

                //Start task if status is ToDo
                if (Status == TaskStatus.ToDo || Status == TaskStatus.Blocked)
                {
                    Status = TaskStatus.InProgress;

                    //Assign user to task if not already assigned                
                    if (AssignedTo == "none")
                    {
                        AssignedTo = username;
                    }

                    return true;
                }
                else
                {
                    throw new Exception($"Only task with status: ToDo or Blocked can be started.");
                }
                
                //Return false if task is already in progress
                return false;
            }
            catch (Exception e)
            {
                throw new Exception($"\n  Error: Start task: {e.Message}");
            }
        }
        
        //Update task status : ITrackable
        public bool UpdateStatus(TaskStatus newStatus)
        {
            if (Status != newStatus)
            {
                Status = newStatus;
                return true;
            }

            return false;
        }

        //Assign user to task
        public void AssignUser(string username)
        {
            AssignedTo = username;
        }

        //Complete task by a user : Itrackable
        public bool CompleteTask(string username)
        {
            if (Status == TaskStatus.InProgress)
            {
                Status = TaskStatus.Completed;
                return true;
            }
            return false;
        }
    }
}
