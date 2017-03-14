using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceOrder.Logic.Services
{
    public interface IService<T>
    {
        void Add(T item);
        void Delete(int? id);
        void Update(T item);
        T Get(int? id);
        IEnumerable<T> GetAll();
        void Dispose();
    }
}
