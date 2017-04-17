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
    public class ServiceProviderRepository: IRepository<ServiceProvider,string>
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
                .Include(o => o.ProviderServiceTypes)
                .Include(o => o.ProviderUser);
        }

        public ServiceProvider Get(string id)
        {
            return db.ServiceProviders.Find(id);
        }

        public IEnumerable<ServiceProvider> Find(Func<ServiceProvider, bool> predicate)
        {
            return db.ServiceProviders
                .Include(o => o.ProviderPhotos)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderServiceTypes)
                .Include(o => o.ProviderUser)
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

        public void Delete(string id)
        {
            ServiceProvider provider = db.ServiceProviders.Find(id);
            if (provider != null)
            {
                db.ServiceProviders.Remove(provider);
            }
        }
    }
}
