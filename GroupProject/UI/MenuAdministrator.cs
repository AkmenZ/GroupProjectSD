using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class MenuAdministrator : Menu
    {
        public override void DisplayMenu()
        {
            //code moved from Program.cs if structure - Tim 17/12/2024 20:07
            Console.WriteLine("  [1] Create New Team Member");
            Console.WriteLine("  [2] Create New Intern");
            Console.WriteLine("  [3] Create New Manager");
            Console.WriteLine("  [4] Create New Administrator");
            Console.WriteLine("  [5] List All Users");
            Console.WriteLine("  [6] Delete User");
            Console.WriteLine("  [7] Change Password");
            Console.WriteLine("  [8] View Logs");
            Console.WriteLine("  [0] Logout");
        }

        //adding paramaters int choice,IServicesUI servicesUI from Menu
        public override void HandleMenuChoice(int choice, IServicesUI servicesUI)
        {
            switch (choice)
            {
                //Perhaps user creation should be done through 1 option with a role selection
                case 1:
                    servicesUI.CreateUser(UserRole.TeamMember);
                    break;
                case 2:
                    servicesUI.CreateUser(UserRole.Intern);
                    break;
                case 3:
                    servicesUI.CreateUser(UserRole.Manager);
                    break;
                case 4:
                    servicesUI.CreateUser(UserRole.Administrator);
                    break;
                case 5:
                    servicesUI.ListAllUsers();  
                    break;
                case 6:
                    servicesUI.DeleteUser();
                    break;
                case 7:
                    servicesUI.ChangePassword();
                    break;
                case 8:
                    servicesUI.ViewLogs();
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
