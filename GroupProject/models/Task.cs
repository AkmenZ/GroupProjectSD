using System;
using Newtonsoft.Json;  

namespace ProjectManagementApp
{
    public abstract class Task : ITask
    {        
        [JsonProperty]
        public string TaskID { get; protected set; }
        [JsonProperty]
        public string EpicID { get; protected set; }
        [JsonProperty]
        public TaskType Type { get; protected set; }
        [JsonProperty]
        public string Title { get; protected set; }
        [JsonProperty]
        public string Description { get; protected set; }
        [JsonProperty]
        public string AssignedTo { get; protected set; }
        [JsonProperty]
        public TaskStatus Status { get; protected set; }
        [JsonProperty]
        public int ProjectID { get; protected set; }

        //Parameterless constructor for JSON serialization
        [JsonConstructor]
        public Task() { }

        public Task(int nextTaskID, string title, string description, string assignedTo, int projectID)
        {            
            TaskID = $"{nextTaskID}.{projectID}";
            Title = title;
            Description = description;
            AssignedTo = assignedTo;
            Status = TaskStatus.Backlog;
            ProjectID = projectID;
        }
  
        //Start task by a user : Itrackable
        public bool StartTask(string currentUsername)
        {
            try
            {
                //Start task if status is ToDo
                if (Status == TaskStatus.ToDo || Status == TaskStatus.Blocked)
                {
                    Status = TaskStatus.InProgress;

                    //Assign user to task if not already assigned                
                    if (AssignedTo == "none")
                    {
                        AssignedTo = currentUsername;
                    }

                    return true;
                }
                else
                {
                    throw new Exception($"Only task with status: ToDo or Blocked can be started.");
                }                
               
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

        //Assign task to epic
        public void AssignToEpic(string epicID)
        {
            EpicID = epicID;
        }

        //Complete task by a user : Itrackable
        public bool CompleteTask(string currentUsername)
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
