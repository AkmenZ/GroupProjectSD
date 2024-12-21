using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class MenuIntern : Menu 
    {
        public override void DisplayMenu()
        {
            Console.WriteLine("  [1] List Assigned Tasks by Project");
            Console.WriteLine("  [2] Update Task Status");
            Console.WriteLine("  [3] Start Task");
            Console.WriteLine("  [4] Complete Task");
            Console.WriteLine("  [5] Change Password");
            Console.WriteLine("  [0] Logout");
        }
        public override void HandleMenuChoice(int choice, IServicesUI servicesUI)
        {
            switch (choice)
            {
                case 1:
                    servicesUI.ListUserTasks();
                    break;
                case 2:
                    servicesUI.UpdateTaskStatus();
                    break;
                case 3:
                    servicesUI.StartTask();
                    break;
                case 4:
                    servicesUI.CompleteTask();
                    break;
                case 5:
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
