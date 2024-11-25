using System.Collections.Generic;

namespace ProjectManagementApp
{
    public interface ILoggerService
    {
        void LogAction(string item, string itemId, string action);
        IReadOnlyList<Log> GetLogs();
    }
}