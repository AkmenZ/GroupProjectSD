﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectManagementApp
{
    public class LoggerService
    {
        //Log file path
        private readonly string _logFilePath = "log.txt";

        //Log action
        public void LogAction(string action, string currentRole, string username)
        {
            try
            {
                //Create log entry
                var logEntry = $"{DateTime.Now} : {action} By: {username} : {currentRole}";
                //Write to log file
                File.AppendAllLines(_logFilePath, new[] { logEntry });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {_logFilePath} {ex.Message}");
            }
        }

        //Get all logs
        public List<string> GetLogs()
        {
            try
            {
                //Check if log file exists
                if (File.Exists(_logFilePath))
                {
                    //Read all lines from log file, return as list
                    return File.ReadAllLines(_logFilePath).ToList();
                }
                //Return empty list if log file does not exist
                return new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading log file: {_logFilePath} {ex.Message}");
                return new List<string>();
            }
        }
    }
}
