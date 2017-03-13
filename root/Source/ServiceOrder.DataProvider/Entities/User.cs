using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ServiceOrder.DataProvider.Entities
{
    public class User : IdentityUser
    {
        public int PhotoId { get; set; }

        public Photo UserPhoto { get; set; }    

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
 
            return userIdentity;
        }
    }
}
