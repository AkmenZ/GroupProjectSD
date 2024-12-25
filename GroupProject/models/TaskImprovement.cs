using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskImprovement : Task
    {
        //Properties unique to TaskImprovement
        [JsonProperty]
        public string RelatedItems { get; private set; }
        //Paramterless constructor for JSON serialization
        public TaskImprovement() { }
        //Constructor for creating TaskImprovement
        public TaskImprovement(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID, double? estimatedStoryPoints, DateTime? dueDate, string relatedItems)
            : base(nextTaskID, title, description, priority, assignedTo, projectID, estimatedStoryPoints, dueDate)
        {
            Type = TaskType.Improvement;
            RelatedItems = relatedItems;
        }
    }
}
