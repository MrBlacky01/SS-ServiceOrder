using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                .Include(o => o.ProviderAlbums)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderServiceTypes.Select(src => src.Category))
                .Include(o => o.ProviderAlbums)
                .Include(o => o.ProviderUser)
                .Include(o => o.ProviderUser.UserPhoto);
        }

        public ServiceProvider Get(string id)
        {
            return db.ServiceProviders
                .Include(o => o.ProviderAlbums)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderAlbums)
                .Include(o => o.ProviderServiceTypes.Select(src => src.Category))
                .Include(o=>o.ProviderUser)
                .Include(o => o.ProviderUser.UserPhoto)
                .FirstOrDefault(e => e.UserId==id);
        }

        public IEnumerable<ServiceProvider> Find(Func<ServiceProvider, bool> predicate)
        {
            return db.ServiceProviders
                .Include(o => o.ProviderAlbums)
                .Include(o => o.ProviderRegions)
                .Include(o => o.ProviderAlbums)
                .Include(o => o.ProviderServiceTypes.Select(src => src.Category))
                .Include(o => o.ProviderUser)
                .Include(o => o.ProviderUser.UserPhoto)
                .Where(predicate).ToList();
        }

        public void Create(ServiceProvider item)
        {
            db.ServiceProviders.Add(item);
        }

        public void Update(ServiceProvider item)
        {
            var entity = Get(item.UserId);
            if (entity == null)
            {
                throw new Exception("No such service provider");
            }
            else
            {
                entity.Description = item.Description;
                entity.WorkingTime = item.WorkingTime;
                if (item.ProviderUser != null)
                {
                    entity.ProviderUser.UserPhoto = item.ProviderUser.UserPhoto;
                }         
                ManyToManyCopierer<Region>.CopyList(item.ProviderRegions,entity.ProviderRegions,db.Regions);
                ManyToManyCopierer<ServiceType>.CopyList(item.ProviderServiceTypes,entity.ProviderServiceTypes,db.ServiceTypes);
                ManyToManyCopierer<Album>.CopyList(item.ProviderAlbums,entity.ProviderAlbums,db.Albums);
                
            }
        }

        public void Delete(int? id)
        {
            ServiceProvider provider = db.ServiceProviders.Find(id);
            if (provider != null)
            {
                db.ServiceProviders.Remove(provider);
            }
        }
    }
}
