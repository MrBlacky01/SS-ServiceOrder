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
    public class AlbumRepository : IRepository<Album, int>
    {
        private ServiceOrderContext db;

        public AlbumRepository(ServiceOrderContext context)
        {
            db = context;
        }


        public IEnumerable<Album> GetAll()
        {
            return db.Albums
                .Include(o => o.Provider)
                .Include(o => o.AlbumPhotos);
        }

        public Album Get(int id)
        {
            return db.Albums
                .Include(o => o.Provider)
                .Include(o => o.AlbumPhotos)
                .FirstOrDefault(src => src.Id == id);

        }

        public IEnumerable<Album> Find(Func<Album, bool> predicate)
        {
            return db.Albums
                .Include(o => o.Provider)
                .Include(o => o.AlbumPhotos)
                .Where(predicate);
        }

        public void Create(Album item)
        {
            item.Provider = db.ServiceProviders.First(opt => opt.UserId == item.Provider.UserId);
            db.Albums.Add(item);
        }

        public void Update(Album item)
        {
            var entity = db.Albums.Where(c => c.Id == item.Id).AsQueryable().FirstOrDefault();
            if (entity == null)
            {
                db.Albums.Add(item);
            }
            else
            {
                entity.Title = item.Title;
                ManyToManyCopierer<Photo>.CopyList(item.AlbumPhotos, entity.AlbumPhotos, db.Photos);
            }
        }

        public void Delete(int? id)
        {
            Album album = db.Albums.FirstOrDefault(src => src.Id == id);
            if (album != null)
            {
                db.Photos.RemoveRange(album.AlbumPhotos);
                db.Albums.Remove(album);
            }
        }
    }
}
