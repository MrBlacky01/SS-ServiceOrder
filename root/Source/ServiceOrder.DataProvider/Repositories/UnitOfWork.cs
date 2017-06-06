using System;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ServiceOrderContext db;
        private OrderRepository orderRepository;
        private ClientRepository clientRepository;
        private PhotoRepository photoRepository;
        private RegionRepository regionRepository;
        private ServiceCategoryRepository categoryRepository;
        private ServiceTypeRepository serviceTypeRepository;
        private ServiceProviderRepository providerRepository;
        private AlbumRepository albumRepository;

        public UnitOfWork(string connectionString)
        {
            db = new ServiceOrderContext();
        }

        public IRepository<User,string> Userss { get; }

        public IRepository<Client,string> Clients => clientRepository ?? (clientRepository = new ClientRepository(db));

        public IRepository<ServiceProvider,string> ServiceProviders
            => providerRepository ?? (providerRepository = new ServiceProviderRepository(db));

        public IRepository<Photo,int> Photos => photoRepository ?? (photoRepository = new PhotoRepository(db));

        public IRepository<Region, int> Regions => regionRepository ?? (regionRepository = new RegionRepository(db));

        public IRepository<Order, int> Orders => orderRepository ?? (orderRepository = new OrderRepository(db));

        public IRepository<ServiceCategory, int> ServiceCategories
            => categoryRepository ?? (categoryRepository = new ServiceCategoryRepository(db));

        public IRepository<ServiceType, int> ServiceTypes
            => serviceTypeRepository ?? (serviceTypeRepository = new ServiceTypeRepository(db));

        public IRepository<Album, int> Albums => albumRepository ?? (albumRepository = new AlbumRepository(db));

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
