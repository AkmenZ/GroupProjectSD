using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskIdea : Task
    {
        //Paramterless constructor for JSON serialization
        public TaskIdea() { }
        //Constructor for creating TaskIdea
        public TaskIdea(int nextTaskID, string title, string description, string assignedTo, int projectID)
            : base(nextTaskID, title, description, assignedTo, projectID)
        {
            Type = TaskType.Idea;
        }
    }
}
