﻿using System;
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
        public TaskBug(int nextTaskID, string title, string description, string assignedTo, int projectID)
            : base(nextTaskID, title, description, assignedTo, projectID)
        {
            Type = TaskType.Bug;
        }
    }
}
