using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class ILog
    {
        int LogId { get; }
        DateTime Time { get; }
        string Item { get; }
        string ItemID { get; }
        String Action { get; }        
        string Username { get; }
        UserRole Role { get; }
    }
}
