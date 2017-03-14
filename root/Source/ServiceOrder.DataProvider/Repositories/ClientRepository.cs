using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private ServiceOrderContext db;

        public ClientRepository(ServiceOrderContext context)
        {
            db = context;
        } 

        public IEnumerable<Client> GetAll()
        {
            return db.Clients.Include(o => o.ClientUser);
        }

        public Client Get(int id)
        {
            return db.Clients.Find(id);
        }

        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return db.Clients.Include(o => o.ClientUser).Where(predicate).ToList();
        }

        public void Create(Client item)
        {
            db.Clients.Add(item);
        }

        public void Update(Client item)
        {
           db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Client client = db.Clients.Find(id);
            if (client != null)
            {
                db.Clients.Remove(client);
            }
        }
    }
}
