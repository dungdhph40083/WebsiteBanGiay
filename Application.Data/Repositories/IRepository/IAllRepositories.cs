using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories.IRepository
{
    public interface IAllRepositories<T> where T : class
    {
        public bool CreateItem(T item);
        public bool UpdateItem(T item);
        public bool DeleteItem(T item);
        IEnumerable<T> GetAllItems();
    }
}
