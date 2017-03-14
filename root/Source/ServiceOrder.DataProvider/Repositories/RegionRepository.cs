using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class RegionRepository : IRepository<Region>
    {
        private ServiceOrderContext db;

        public RegionRepository(ServiceOrderContext context)
        {
            db = context;
        }

        public IEnumerable<Region> GetAll()
        {
            return db.Regions;
        }

        public Region Get(int id)
        {
            return db.Regions.Find(id);
        }

        public IEnumerable<Region> Find(Func<Region, bool> predicate)
        {
            return db.Regions.Where(predicate).ToList();
        }

        public void Create(Region item)
        {
            db.Regions.Add(item);
        }

        public void Update(Region item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Region region = db.Regions.Find(id);
            if (region != null)
            {
                db.Regions.Remove(region);
            }
        }
    }
}
