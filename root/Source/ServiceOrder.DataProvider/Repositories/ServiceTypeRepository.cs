using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class ServiceTypeRepository : IRepository<ServiceType,int>
    {
        private ServiceOrderContext db;

        public ServiceTypeRepository(ServiceOrderContext context)
        {
            db = context;
        }

        public IEnumerable<ServiceType> GetAll()
        {
            return db.ServiceTypes.Include(o => o.Category);
        }

        public ServiceType Get(int id)
        {
            return db.ServiceTypes
                .Include(o => o.Category)
                .First(p => p.Id == id);
        }

        public IEnumerable<ServiceType> Find(Func<ServiceType, bool> predicate)
        {
            return db.ServiceTypes
                .Include(o => o.Category)
                .Where(predicate).ToList();
        }

        public void Create(ServiceType item)
        {
            item.Category = db.ServiceCategories.First(c => c.Id == item.Category.Id);
            db.ServiceTypes.Add(item);
        }

        public void Update(ServiceType item)
        {
            var entity = db.ServiceTypes.Where(c => c.Id == item.Id).AsQueryable().FirstOrDefault();
            if (entity == null)
            {
                db.ServiceTypes.Add(item);
            }
            else
            {
                entity.Title = item.Title;
                entity.Category = db.ServiceCategories.First(c => c.Id == item.Category.Id);
            }
        }

        public void Delete(int id)
        {
            var service = db.ServiceTypes.Find(id);
            if (service != null)
            {
                db.ServiceTypes.Remove(service);
            }
        }
    }
}
