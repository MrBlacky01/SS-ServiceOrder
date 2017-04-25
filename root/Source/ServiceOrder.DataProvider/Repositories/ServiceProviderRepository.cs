using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.DataProvider.Utils;

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
                .Include(o => o.ProviderServiceTypes.Select(src => src.Category))
                .Include(o => o.ProviderUser);
        }

        public ServiceProvider Get(string id)
        {
            return db.ServiceProviders
                .Include(o => o.ProviderPhotos)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderServiceTypes.Select(src => src.Category))
                .Include(o=>o.ProviderUser)
                .First(e => e.UserId==id);
        }

        public IEnumerable<ServiceProvider> Find(Func<ServiceProvider, bool> predicate)
        {
            return db.ServiceProviders
                .Include(o => o.ProviderPhotos)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderServiceTypes.Select(src => src.Category))
                .Include(o => o.ProviderUser)
                .Where(predicate).ToList();
        }

        public void Create(ServiceProvider item)
        {
            db.ServiceProviders.Add(item);
        }

        public void Update(ServiceProvider item)
        {
            var entity = db.ServiceProviders.Where(c => c.UserId == item.UserId).AsQueryable().FirstOrDefault();
            if (entity == null)
            {
                db.ServiceProviders.Add(item);
            }
            else
            {
                entity.Description = item.Description;
                entity.WorkingTime = item.WorkingTime;
                ManyToManyCopierer<Region>.CopyList(item.ProviderRegions,entity.ProviderRegions,db.Regions);
                ManyToManyCopierer<ServiceType>.CopyList(item.ProviderServiceTypes,entity.ProviderServiceTypes,db.ServiceTypes);
                ManyToManyCopierer<Photo>.CopyList(item.ProviderPhotos,entity.ProviderPhotos,db.Photos);
                
            }
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
