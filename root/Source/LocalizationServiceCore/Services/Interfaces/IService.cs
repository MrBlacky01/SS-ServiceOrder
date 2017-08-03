using System.Collections.Generic;

namespace LocalizationServiceCore.Services.Interfaces
{
    public interface IService<T> where T : class
    {
        void Create(T element);
        IEnumerable<T> GetAll();

        T GetById<TId>(TId id);

        void Update(T element);

        void Delete<TId>(TId id);
    }
}
