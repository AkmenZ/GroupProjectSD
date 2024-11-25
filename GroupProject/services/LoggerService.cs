using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectManagementApp
{
    public class LoggerService : ILoggerService
    {
        //Property to store authentication service
        private readonly IAuthService _authService;
        //Property to store Log repository
        private readonly IRepository<Log> _logRepository;

        //Constructor, dependency injection
        public LoggerService(IRepository<Log> logRepository, IAuthService authService)
        {
            _logRepository = logRepository;
            _authService = authService;
        }

        //Log action
        public void LogAction(string item, string itemId, string action)
        {
            try
            {
                //Get data
                var logs = _logRepository.GetAll();
                //Determine the next log ID
                int nextLogID = logs.Select(l => l.LogID).DefaultIfEmpty(0).Max() + 1;
                //Create new log
                var log = new Log(nextLogID, itemId, item, action, _authService.CurrentUsername, _authService.CurrentUserRole);
                //Add log to logs list
                logs.Add(log);
                //Save changes
                _logRepository.SaveAll(logs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging action: {ex.Message}");
            }

        }

        //Get all logs
        public IReadOnlyList<Log> GetLogs()
        {
            try
            {
                return _logRepository.GetAll().ToList().AsReadOnly();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting logs: {ex.Message}");
            }

        }
    }
}
