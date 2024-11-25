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
        public abstract void HandleMenuChoice();

    }
}
