using System;
using System.Collections.Generic;

namespace ServiceOrder.Logic.Services
{
    public interface IService<T,W>
    {
        void Add(T item);
        void Delete(W id);
        void Update(T item);
        T Get(W id);
        IEnumerable<T> GetAll();
        void Dispose();
    }
}
