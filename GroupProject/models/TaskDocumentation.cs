using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models
{
    public class TaskDocumentation : Task
    {
        //Properties unique to TaskDocumentation
        [JsonProperty]
        public string Audience { get; private set; }
        [JsonProperty]
        public string RelatedItems { get; private set; }
        //Paramterless constructor for JSON serialization
        public TaskDocumentation() { }
        //Constructor for creating TaskDocumentation
        public TaskDocumentation(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID, double? estimatedStoryPoints, DateTime? dueDate, string audience, string relatedItems)
            : base(nextTaskID, title, description, priority, assignedTo, projectID, estimatedStoryPoints, dueDate)
        {
            Type = TaskType.Documentation;
            Audience = audience;
            RelatedItems = relatedItems;
        }
    }
}
