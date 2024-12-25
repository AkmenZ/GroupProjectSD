using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskBug : Task
    {
        //Properties unique to TaskBug
        [JsonProperty]
        public string StepsToReproduce { get; private set; }
        [JsonProperty]
        public string ReportedBy { get; private set; }
        //Parameterless constructor for JSON serialization
        public TaskBug() { }
        //Constructor for creating TaskBug
        public TaskBug(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID, double? estimatedStoryPoints, DateTime? dueDate, string stepsToReproduce, string reportedBy)
            : base(nextTaskID, title, description, priority, assignedTo, projectID, estimatedStoryPoints, dueDate)
        {
            Type = TaskType.Bug;
            StepsToReproduce = stepsToReproduce;
            ReportedBy = reportedBy;
        }        
    }
}
