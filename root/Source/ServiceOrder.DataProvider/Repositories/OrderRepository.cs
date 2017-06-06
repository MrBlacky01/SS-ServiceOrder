using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceOrder.DataProvider.DataBase;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;

namespace ServiceOrder.DataProvider.Repositories
{
    public class OrderRepository : IRepository<Order,int>
    {
        private ServiceOrderContext db;

        public OrderRepository(ServiceOrderContext context)
        {
            db = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders.Include(c => c.OrderClient)
                .Include(p => p.OrderProvider)
                .Include(r => r.OrderRegion)
                .Include(st => st.OrderType);
        }

        public Order Get(int id)
        {
            return db.Orders.
                Include(c => c.OrderClient)
                .Include(p => p.OrderProvider)
                .Include(r => r.OrderRegion)
                .Include(st => st.OrderType)
                .FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return db.Orders
                .Include(c => c.OrderClient)
                .Include(p => p.OrderProvider)
                .Include(r => r.OrderRegion)
                .Include(st => st.OrderType)
                .Where(predicate).ToList();
        }

        public void Create(Order item)
        {
            item.OrderClient = db.Clients.FirstOrDefault(c => c.UserId == item.OrderClient.UserId);
            item.OrderProvider = db.ServiceProviders.FirstOrDefault(c => c.UserId == item.OrderProvider.UserId);
            item.OrderRegion = db.Regions.FirstOrDefault(c => c.Id == item.OrderRegion.Id);
            item.OrderType = db.ServiceTypes.FirstOrDefault(c => c.Id == item.OrderType.Id);
            db.Orders.Add(item);
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
            }
        }
    }
}
