using System;
using System.Collections.Generic;

namespace ServiceOrder.DataProvider.Interfaces
{
    public interface IRepository<T,W> where T : class 
    {
        IEnumerable<T> GetAll();
        T Get(W id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int? id);
    }
}
