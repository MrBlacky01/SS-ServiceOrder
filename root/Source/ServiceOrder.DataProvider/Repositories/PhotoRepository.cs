using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class PhotoRepository : IRepository<Photo>
    {
        private ServiceOrderContext db;

        public PhotoRepository(ServiceOrderContext context)
        {
            db = context;
        }

        public IEnumerable<Photo> GetAll()
        {
            return db.Photos;
        }

        public Photo Get(int id)
        {
            return db.Photos.Find(id);
        }

        public IEnumerable<Photo> Find(Func<Photo, bool> predicate)
        {
            return db.Photos.Where(predicate).ToList();
        }

        public void Create(Photo item)
        {
            db.Photos.Add(item);
        }

        public void Update(Photo item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Photo photo = db.Photos.Find(id);
            if (photo != null)
            {
                db.Photos.Remove(photo);
            }
        }
    }
}
