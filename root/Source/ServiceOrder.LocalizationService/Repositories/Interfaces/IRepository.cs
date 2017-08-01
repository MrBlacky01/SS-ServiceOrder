using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace ServiceOrder.LocalizationService.Repositories.Interfaces
{
    public interface IRepository<T> where T:class 
    {  
        IEnumerable<T> GetAll();

        T GetById(ObjectId id);

        void Create(T element);

        void Update(T element);

        void Delete(ObjectId id);
        T Find(Func<T, bool> predicate);
    }
}
