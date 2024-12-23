using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectManagementApp
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        private readonly string _filePath;
        private readonly JsonSerializerSettings _jsonSettings;
        private static readonly object _lock = new object();

        public DataRepository(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
        }

        public List<T> GetAll()
        {
            lock (_lock)
            {
                if (!File.Exists(_filePath))
                {
                    return new List<T>();
                }
                var jsonData = File.ReadAllText(_filePath);
                return string.IsNullOrWhiteSpace(jsonData)
                    ? new List<T>()
                    : JsonConvert.DeserializeObject<List<T>>(jsonData, _jsonSettings) ?? new List<T>();
            }
        }

        public void SaveAll(List<T> items)
        {
            lock (_lock)
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

        public void Add(T item)
        {
            lock (_lock)
            {
                try
                {
                    var items = GetAll();
                    items.Add(item);
                    SaveAll(items);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error adding item to file: {_filePath} {ex.Message}");
                }
            }
        }

        public void Remove(Func<T, bool> predicate)
        {
            lock (_lock)
            {
                try
                {
                    var items = GetAll();
                    var itemToRemove = items.FirstOrDefault(predicate);
                    if (itemToRemove != null)
                    {
                        items.Remove(itemToRemove);
                        SaveAll(items);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error removing item from file: {_filePath} {ex.Message}");
                }
            }
        }
    }
}
