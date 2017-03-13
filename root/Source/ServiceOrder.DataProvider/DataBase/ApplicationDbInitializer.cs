using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ServiceOrder.DataProvider.Entities;

namespace ServiceOrder.DataProvider.DataBase
{
    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ServiceOrderContext>
    {
        protected override void Seed(ServiceOrderContext context)
        {
            var userManager = new UserManager(new UserStore<User>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole {Name = "service provider"};
            var role3 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

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

            base.Seed(context);
        }
    }
}
