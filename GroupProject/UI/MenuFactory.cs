using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    /*
     * Creating a simple factory to abstract the creation of menus 
     * based on different user roles. Demonstrating a practical use of 
     * abstract classes. 
     * Source: https://www.linkedin.com/pulse/factory-method-pattern-c-konstantinos-kalafatis
     * Tim O' Mahony - 19/12/2024 - 21:52
     */

    public class MenuFactory
    {
        public static Menu CeateMenu(UserRole role)
        {
            /* use pattern matching in place of multiple return statemnts
             * source: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/switch-expression
             * Tim O' Mahony - 19/12/2024 - 21:53
             */
            return role switch
            {
                //pattern matching denoted by pattern => action
                //_ pattern if nothing is present

                UserRole.Administrator => new MenuAdministrator(),
                UserRole.Manager => new MenuManager(),
                UserRole.TeamMember => new MenuTeamMember(),
                UserRole.Intern => new MenuIntern(),
                _ => throw new ArgumentException("Invalid user role")

            };


        }
    }
}
