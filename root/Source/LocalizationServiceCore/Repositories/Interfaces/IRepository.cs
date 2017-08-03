using System.Collections.Generic;
using MongoDB.Driver;

namespace LocalizationServiceCore.Repositories.Interfaces
{
    public interface IRepository<T> where T:class 
    {
        void Create(T element);
        IEnumerable<T> Read(FilterDefinition<T> filter);

        void Update(T element, FilterDefinition<T> filter);

        void Delete(FilterDefinition<T> filter);
    }
}
