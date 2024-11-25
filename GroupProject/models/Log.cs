using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public class Log : ILog
    {
        [JsonProperty]
        public int LogID { get; private set; }
        [JsonProperty]
        public DateTime Time { get; private set; }
        [JsonProperty]
        public string Item { get; private set; }
        [JsonProperty]
        public string ItemID { get; private set; }
        [JsonProperty]
        public String Action { get; private set; }
        [JsonProperty]
        public string Username { get; private set; }
        [JsonProperty]
        public UserRole Role { get; private set; }

        [JsonConstructor]
        public Log(int nextLogID, string itemId, string item, string action, string username, UserRole role)
        {
            LogID = nextLogID++;
            Time = DateTime.Now;
            Item = item;
            ItemID = itemId;
            Action = action;
            Username = username;
            Role = role;
        }   
    }
}
