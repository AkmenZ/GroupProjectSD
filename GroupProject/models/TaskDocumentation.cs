﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp.Models
{
    public class TaskDocumentation : Task
    {
        //Paramterless constructor for JSON serialization
        public TaskDocumentation() { }
        //Constructor for creating TaskDocumentation
        public TaskDocumentation(int nextTaskID, string title, string description, string assignedTo, int projectID)
            : base(nextTaskID, title, description, assignedTo, projectID)
        {
            Type = TaskType.Documentation;
        }
    }
}
