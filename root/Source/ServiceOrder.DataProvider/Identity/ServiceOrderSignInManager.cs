using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ServiceOrder.DataProvider.Entities;

namespace ServiceOrder.DataProvider.Identity
{
    public class ServiceOrderSignInManager : SignInManager<User, string>
    {
        public ServiceOrderSignInManager(ServiceOrderUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((ServiceOrderUserManager)UserManager);
        }

        public static ServiceOrderSignInManager Create(IdentityFactoryOptions<ServiceOrderSignInManager> options, IOwinContext context)
        {
            return new ServiceOrderSignInManager(OwinContextExtensions.GetUserManager<ServiceOrderUserManager>(context), context.Authentication);
        }
    }
}
