using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskFeature : Task
    {
        //Paramterless constructor for JSON serialization
        public TaskFeature() { }
        //Constructor for creating TaskFeature
        public TaskFeature(int nextTaskID, string title, string description, string assignedTo, int projectID)
            : base(nextTaskID, title, description, assignedTo, projectID)
        {
            Type = TaskType.Feature;
        }
    }
}
