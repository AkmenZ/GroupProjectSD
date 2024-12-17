using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public abstract class Menu : IMenu
    {
        public abstract void DisplayMenu();

        //added int choice,IServicesUI servicesUI - Tim 17/12/2024 - 21:01
        public abstract void HandleMenuChoice(int choice, IServicesUI servicesUI);

    }
}
