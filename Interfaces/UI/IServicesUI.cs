using System;
using Spectre.Console;
using Spectre.Console.Cli;

namespace ProjectManagementApp
{
    public interface IServicesUI
    {
        void Login();
        void Logout();
        void CreateUser(UserRole role);
        void DeleteUser();
        void ListAllUsers();
        void CreateProject();
        void AddTeamMemberToProject();
        void UpdateProjectStatus();
        void DeleteProject();
        void AddTask();
        void AssignUserToTask();
        void AssignTaskToEpic();
        void StartTask();
        void CompleteTask();
        void UpdateTaskStatus();
        void DeleteTask();
        void ListUserTasks();
        void ListAllTasks(); 
        void ListTasksByManager();
        void ListProjectTasks();
        void ListProjectTasksByStatus();
        void ListEpicTasks();
        void ListProjectTasksByType();
        void ListProjectTasksByUser();
        void ListAllProjects();
        void ListProjectsByUser();
        void ListProjectsByManager();
        void ListProjectsByStatus();
        void ListProjectTeamMembers();
        void ChangePassword();
        ProjectStatus SelectProjectStatus();
        TaskType SelectTaskType();
        TaskStatus SelectTaskStatus();
        void PrintTasks(IReadOnlyList<Task> tasks);
        void ViewLogs();        
    }
}

