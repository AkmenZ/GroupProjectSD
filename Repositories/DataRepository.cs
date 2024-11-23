using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjectManagementApp
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        //Attribute to store JSON file path
        private readonly string _filePath;

        //Attribute to store JSON serialization settings
        private readonly JsonSerializerSettings _jsonSettings;

        //Constructor accepting JSON file path, setting JSON serialization settings
        public DataRepository(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        //Get all items from JSON file
        public List<T> GetAll()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<T>();
                }
                var jsonData = File.ReadAllText(_filePath);

                if (string.IsNullOrWhiteSpace(jsonData))
                {
                    return new List<T>();
                }

                return JsonConvert.DeserializeObject<List<T>>(jsonData, _jsonSettings) ?? new List<T>();
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error deserializing data from file: {_filePath} {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading data from file: {_filePath} {ex.Message}");
            }
        }

        //Save all items to JSON file
        public void SaveAll(List<T> items)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(items, Formatting.Indented, _jsonSettings);
                File.WriteAllText(_filePath, jsonData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving data to file: {_filePath} {ex.Message}");
            }
        }
    }
}