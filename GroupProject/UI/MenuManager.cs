using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class MenuManager : Menu
    {
        public override void DisplayMenu()
        {
            //code moved from Program.cs, replacing part of an if structure

            Console.WriteLine("  Project Actions:         View Projects:          View Tasks:                         Task Actions:");
            Console.WriteLine("  ---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("  [1]  Create              [5]  All                [10] All                            [17] Add");
            Console.WriteLine("  [2]  Add Team Member     [6]  By Team Member     [11] By Project                     [18] Assign Team Member");
            Console.WriteLine("  [3]  Update Status       [7]  By Manager         [12] By Manager                     [19] Assign Task to Epic");
            Console.WriteLine("  [4]  Delete              [8]  By Status          [13] By Project And Status          [20] Start");
            Console.WriteLine("                           [9]  Team Members       [14] By Project And Type            [21] Update Status");
            Console.WriteLine("  ==============================================   [15] By Project And Team Member     [22] Complete");
            Console.WriteLine("  [23] Change Password     [0]  Logout             [16] By Epic                        [23] Delete");


        }
        // added int choice,IServicesUI servicesUI from Menu - Tim 17/12/2024 - 21:03
        public override void HandleMenuChoice(int choice, IServicesUI servicesUI)
        {
            //switch statment copied from Program.cs - Tim 17/12/2024 - 21:38
            switch (choice)
            {
                case 1:
                    servicesUI.CreateProject();
                    break;
                case 2:
                    servicesUI.AddTeamMemberToProject();
                    break;
                case 3:
                    servicesUI.UpdateProjectStatus();
                    break;
                case 4:
                    servicesUI.DeleteProject();
                    break;
                case 5:
                    servicesUI.ListAllProjects();
                    break;
                case 6:
                    servicesUI.ListProjectsByUser();
                    break;
                case 7:
                    servicesUI.ListProjectsByManager();
                    break;
                case 8:
                    servicesUI.ListProjectsByStatus();
                    break;
                case 9:
                    servicesUI.ListProjectTeamMembers();
                    break;
                case 10:
                    servicesUI.ListAllTasks();
                    break;
                case 11:
                    servicesUI.ListProjectTasks();
                    break;
                case 12:
                    servicesUI.ListTasksByManager();
                    break;
                case 13:
                    servicesUI.ListProjectTasksByStatus();
                    break;
                case 14:
                    servicesUI.ListProjectTasksByType();
                    break;
                case 15:
                    servicesUI.ListProjectTasksByUser();
                    break;
                case 16:
                    servicesUI.ListEpicTasks();
                    break;
                case 17:
                    servicesUI.AddTask();
                    break;
                case 18:
                    servicesUI.AssignUserToTask();
                    break;
                case 19:
                    servicesUI.AssignTaskToEpic();
                    break;
                case 20:
                    servicesUI.StartTask();
                    break;
                case 21:
                    servicesUI.UpdateTaskStatus();
                    break;
                case 22:
                    servicesUI.CompleteTask();
                    break;
                case 23:
                    servicesUI.DeleteTask();
                    break;
                case 24:
                    servicesUI.ChangePassword();
                    break;
                case 0:
                    servicesUI.Logout();
                    break;
                default:
                    Console.WriteLine("  Invalid choice. Please try again.\n");
                    break;
            }
        }
    }
}
