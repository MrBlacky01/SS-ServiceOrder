using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Host.SystemWeb;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.AccountViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork DataBase { get; set; }

        public AccountService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
        }

        private ServiceOrderSignInManager _signInManager;
        private ServiceOrderUserManager _userManager;

        public ServiceOrderSignInManager SignInManager {
            get
            {               
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ServiceOrderSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }
        public ServiceOrderUserManager UserManager {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ServiceOrderUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        public async Task<SignInStatus> Login(LoginViewModel model)
        {
            return await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        }

        public async Task<IdentityResult> Register(RegisterViewModel model)
        {
            var user = new User { UserName = model.Name, Email = model.Email };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return result;
            if (model.IsServiceProvider)
            {
                UserManager.AddToRole(user.Id, "service provider");
                DataBase.ServiceProviders.Create(new ServiceProvider() {ProviderUser = user});
            }
            else
            {
                UserManager.AddToRole(user.Id, "client");
                DataBase.Clients.Create(new Client() {ClientUser = user});
            }
            await SignIn(user);
            return result;
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                throw new Exception("There's no such user");
            }
            return await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);          
        }

        public async Task SignIn(User user)
        {
             await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }

        public void Dispose()
        {            
            if (_signInManager != null)
            {
                _signInManager.Dispose();
                _signInManager = null;
            }
            if (_userManager == null) return;
            _userManager.Dispose();
            _userManager = null;
        }

    }
}
