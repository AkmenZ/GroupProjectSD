using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskBug : Task
    {
        //Parameterless constructor for JSON serialization
        public TaskBug() { }
        //Constructor for creating TaskBug
        public TaskBug(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID)
            : base(nextTaskID, title, description, priority, assignedTo, projectID)
        {
            Type = TaskType.Bug;
        }
    }
}
