﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class TaskEpic : Task
    {
        //Paramterless constructor for JSON serialization
        public TaskEpic() { }
        //Constructor for creating a TaskEpic
        public TaskEpic(int nextTaskID, string title, string description, TaskPriority priority, string assignedTo, int projectID, double? estimatedStoryPoints, DateTime? dueDate)
            : base(nextTaskID, title, description, priority, assignedTo, projectID, estimatedStoryPoints, dueDate)
        {
            Type = TaskType.Epic;
            EpicID = TaskID;
        }
    }
}
