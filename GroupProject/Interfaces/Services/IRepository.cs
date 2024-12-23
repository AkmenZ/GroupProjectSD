using System.Collections.Generic;

namespace ProjectManagementApp
{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        void SaveAll(List<T> items);
        void Add(T item); // Add this method
    }
}