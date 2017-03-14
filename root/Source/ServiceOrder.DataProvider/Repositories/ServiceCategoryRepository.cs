using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class ServiceCategoryRepository : IRepository<ServiceCategory>
    {
        private ServiceOrderContext db;

        public ServiceCategoryRepository(ServiceOrderContext context)
        {
            db = context;
        }

        public IEnumerable<ServiceCategory> GetAll()
        {
            return db.ServiceCategories;
        }

        public ServiceCategory Get(int id)
        {
            return db.ServiceCategories.Find(id);
        }

        public IEnumerable<ServiceCategory> Find(Func<ServiceCategory, bool> predicate)
        {
            return db.ServiceCategories
                .Include(o => o.ServicesInCategory)
                .Where(predicate)
                .ToList();
        }

        public void Create(ServiceCategory item)
        {
            db.ServiceCategories.Add(item);
        }

        public void Update(ServiceCategory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var category = db.ServiceCategories.Find(id);
            if (category != null)
            {
                db.ServiceCategories.Remove(category);
            }
        }
    }
}
