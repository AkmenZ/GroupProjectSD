namespace ProjectManagementApp
{
    public interface ILoggerService
    {
        void LogAction(string action);
        List<string> GetLogs();
    }
}