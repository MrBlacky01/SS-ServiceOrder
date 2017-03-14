﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.DataProvider.Entities;

namespace ServiceOrder.DataProvider.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Userss { get; }
        IRepository<Client> Clients { get; }
        IRepository<ServiceProvider> ServiceProviders { get; }
        IRepository<Photo> Photos { get; }
        IRepository<Region> Regions { get; }
        IRepository<Order> Orders { get; }
        IRepository<ServiceCategory> ServiceCategories { get; }
        IRepository<ServiceType> ServiceTypes { get; }
        void Save();
    }
}