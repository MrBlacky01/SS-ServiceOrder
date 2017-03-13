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
    }
}
