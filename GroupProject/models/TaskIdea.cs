using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskIdea : Task
    {
        //Properties unique to TaskIdea
        [JsonProperty]
        public string Author { get; private set; }
        //Paramterless constructor for JSON serialization
        public TaskIdea() { }
        //Constructor for creating TaskIdea
        public TaskIdea(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID, double? estimatedStoryPoints, DateTime? dueDate, string author)
            : base(nextTaskID, title, description, priority, assignedTo, projectID, estimatedStoryPoints, dueDate)
        {
            Type = TaskType.Idea;
            Author = author;
        }
    }
}
