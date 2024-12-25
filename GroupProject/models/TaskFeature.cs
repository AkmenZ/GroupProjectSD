using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskFeature : Task
    {
        //Properties unique to TaskFeature
        [JsonProperty]
        public string AcceptanceCriteria { get; private set; }
        //Paramterless constructor for JSON serialization
        public TaskFeature() { }
        //Constructor for creating TaskFeature
        public TaskFeature(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID, double? estimatedStoryPoints, DateTime? dueDate, string acceptanceCriteria)
            : base(nextTaskID, title, description, priority, assignedTo, projectID, estimatedStoryPoints, dueDate)
        {
            Type = TaskType.Feature;
            AcceptanceCriteria = acceptanceCriteria;
        }
    }
}
