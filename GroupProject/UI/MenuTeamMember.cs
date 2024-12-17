using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class MenuTeamMember : Menu
    {
        public override void DisplayMenu()
        {
            //copied from program.cs - Tim - 17/12/2024 - 21:46
            Console.WriteLine("  [1] List Assigned Tasks by Project");
            Console.WriteLine("  [2] List All Tasks");
            Console.WriteLine("  [3] Update Task Status");
            Console.WriteLine("  [4] Start Task");
            Console.WriteLine("  [5] Complete Task");
            Console.WriteLine("  [6] Change Password");
            Console.WriteLine("  [0] Logout");
        }
        public override void HandleMenuChoice(int choice, IServicesUI servicesUI)
        {
            //switch statement copied from program.cs - Tim - 17/12/2024 - 21:45
            switch (choice)
            {
                case 1:
                    servicesUI.ListUserTasks();
                    break;
                case 2:
                    servicesUI.ListAllTasks();
                    break;
                case 3:
                    servicesUI.UpdateTaskStatus();
                    break;
                case 4:
                    servicesUI.StartTask();
                    break;
                case 5:
                    servicesUI.CompleteTask();
                    break;
                case 6:
                    servicesUI.ChangePassword();
                    break;
                case 0:
                    servicesUI.Logout();
                    break;
                default:
                    Console.WriteLine("\n  Invalid choice.");
                    break;
            }
        }
    }
}
