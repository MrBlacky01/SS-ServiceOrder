using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceOrder.DataProvider.Entities;

namespace ServiceOrder.DataProvider.DataBase
{
    public class ServiceOrderContext : IdentityDbContext<User>
    {
        public ServiceOrderContext() 
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ServiceOrderContext>(new ApplicationDbInitializer());
        }

        public ServiceOrderContext Create()
        {
            return new ServiceOrderContext();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

    }
}
