using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementApp
{
    public interface IRepository<T> where T : class
        {
            List<T> GetAll();
            void SaveAll(List<T> items);
        }
}
