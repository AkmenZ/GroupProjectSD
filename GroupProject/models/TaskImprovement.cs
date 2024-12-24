using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskImprovement : Task
    {
        //Paramterless constructor for JSON serialization
        public TaskImprovement() { }
        //Constructor for creating TaskImprovement
        public TaskImprovement(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID)
            : base(nextTaskID, title, description, priority, assignedTo, projectID)
        {
            Type = TaskType.Improvement;
        }
    }
}
