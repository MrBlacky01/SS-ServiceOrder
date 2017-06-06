using System;
using ServiceOrder.DataProvider.Entities;

namespace ServiceOrder.DataProvider.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User,string> Userss { get; }
        IRepository<Client, string> Clients { get; }
        IRepository<ServiceProvider, string> ServiceProviders { get; }
        IRepository<Photo,int> Photos { get; }
        IRepository<Region, int> Regions { get; }
        IRepository<Order, int> Orders { get; }
        IRepository<ServiceCategory, int> ServiceCategories { get; }
        IRepository<ServiceType, int> ServiceTypes { get; }
        IRepository<Album,int> Albums { get; } 
        void Save();
    }
}
