using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Identity;

namespace ServiceOrder.DataProvider.DataBase
{
    public class ApplicationDbInitializer : CreateDatabaseIfNotExists<ServiceOrderContext>
    {
        protected override void Seed(ServiceOrderContext context)
        {
            var userManager = new ServiceOrderUserManager(new UserStore<User>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole {Name = "service provider"};
            var role3 = new IdentityRole { Name = "client" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // создаем пользователей
            var admin = new User { Email = "adminJ@gmail.com", UserName = "admin" };
            string password = "123321";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
            }

            var client = new User { Email = "w@w.com", UserName = "client" };
            result = userManager.Create(client, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(client.Id, role3.Name);
                context.Clients.Add(new Client() {UserId = client.Id});
            }

            


            ServiceCategory category = new ServiceCategory {Title = "Transportation"};
            ServiceCategory category1 = new ServiceCategory {Title = "Repair of machinery" };
            ServiceCategory category2 = new ServiceCategory {Title = "Cleaning"};
            ServiceCategory category3 = new ServiceCategory {Title = "Computer help"};
            ServiceCategory category4 = new ServiceCategory {Title = "Education" };
            ServiceCategory category5 = new ServiceCategory {Title = "Photo and video" };
            context.ServiceCategories.Add(category);
            context.ServiceCategories.Add(category1);
            context.ServiceCategories.Add(category2);
            context.ServiceCategories.Add(category3);
            context.ServiceCategories.Add(category4);
            context.ServiceCategories.Add(category5);

            var region0 = new Region {Title = "Brest"};
            var region1 = new Region {Title = "Vitebsk"};
            var region2= new Region {Title = "Gomel"};
            var region3 = new Region {Title = "Grodno"};
            var region4 = new Region {Title = "Minsk"};
            var region5 = new Region {Title = "Mogilev"};
            context.Regions.Add(region0);
            context.Regions.Add(region1);
            context.Regions.Add(region2);
            context.Regions.Add(region3);
            context.Regions.Add(region4);
            context.Regions.Add(region5);
            context.SaveChanges();

            var service0 = new ServiceType {Title = "Repair of household appliances", Category = category1};
            var service1 = new ServiceType {Title = "Repair of mobile phones", Category = category1};
            var service2 = new ServiceType {Title = "Repair of audio equipment", Category = category1};
            var service3 = new ServiceType {Title = "Cargo transportation", Category = category};
            var service4 = new ServiceType {Title = "Passenger Transportation", Category = category};
            var service5 = new ServiceType {Title = "Courier", Category = category};
            var service6 = new ServiceType {Title = "Spring-cleaning", Category = category2};
            var service7 = new ServiceType {Title = "Maintenance cleaning", Category = category2};
            var service8 = new ServiceType {Title = "Cleaning after repair", Category = category2};

            context.ServiceTypes.Add(service0);
            context.ServiceTypes.Add(service1);
            context.ServiceTypes.Add(service2);
            context.ServiceTypes.Add(service3);
            context.ServiceTypes.Add(service4);
            context.ServiceTypes.Add(service5);
            context.ServiceTypes.Add(service6);
            context.ServiceTypes.Add(service7);
            context.ServiceTypes.Add(service8);

            var serviceProvider = new User { Email = "q@q.com", UserName = "provider0" };
            result = userManager.Create(serviceProvider, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(serviceProvider.Id, role2.Name);
                context.ServiceProviders.Add(new ServiceProvider() { UserId = serviceProvider.Id, Description = "This is provider 0" 
                    ,ProviderServiceTypes = new List<ServiceType> {service0,service1,service2} });
            }

            var serviceProvider1 = new User { Email = "e@e.com", UserName = "provider1" };
            result = userManager.Create(serviceProvider1, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(serviceProvider1.Id, role2.Name);
                context.ServiceProviders.Add(new ServiceProvider() { UserId = serviceProvider1.Id, Description = "This is provider 1",
                    ProviderServiceTypes = new List<ServiceType> { service3, service4, service5 } });
            }

            base.Seed(context);
        }
    }
}
