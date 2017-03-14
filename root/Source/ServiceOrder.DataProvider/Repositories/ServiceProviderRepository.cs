using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class ServiceProviderRepository: IRepository<ServiceProvider>
    {
        private ServiceOrderContext db;

        public ServiceProviderRepository(ServiceOrderContext context)
        {
            db = context;
        }

        public IEnumerable<ServiceProvider> GetAll()
        {
            return db.ServiceProviders
                .Include(o => o.ProviderPhotos)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderServiceTypes);
        }

        public ServiceProvider Get(int id)
        {
            return db.ServiceProviders.Find(id);
        }

        public IEnumerable<ServiceProvider> Find(Func<ServiceProvider, bool> predicate)
        {
            return db.ServiceProviders
                .Include(o => o.ProviderPhotos)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderServiceTypes)
                .Where(predicate).ToList();
        }

        public void Create(ServiceProvider item)
        {
            db.ServiceProviders.Add(item);
        }

        public void Update(ServiceProvider item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ServiceProvider provider = db.ServiceProviders.Find(id);
            if (provider != null)
            {
                db.ServiceProviders.Remove(provider);
            }
        }
    }
}
