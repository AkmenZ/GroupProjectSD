using System;

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
        void UpdateProjectStatus();
        void DeleteProject();
        void AddTask();
        void AssignUserToTask();
        void StartTask();
        void CompleteTask();
        void UpdateTaskStatus();
        void DeleteTask();
        void ListUserTasks();
        void ListAllTasks();
        void ListAllProjects();
        void ChangePassword();
        void ViewLogs();
    }
}

