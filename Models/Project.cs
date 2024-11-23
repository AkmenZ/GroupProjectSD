using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ProjectManagementApp
{
    public class Project : IProject
    {
        //Properties        
        [JsonProperty]
        public int ProjectID { get; private set; }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public string Manager { get; private set; }
        [JsonProperty]
        public ProjectStatus Status { get; private set; }
        [JsonProperty]
        public string Description { get; private set; }

        //Constructor for creating a new project
        public Project(int nextProjectID, string projectName, string manager, string description)
        {
            ProjectID = nextProjectID;
            Name = projectName;
            Manager = manager;
            Status = ProjectStatus.Created;
            Description = description;
        }
                
        //Update project status
        public void UpdateStatus(ProjectStatus status)
        {
            Status = status;
        }
    }
}
