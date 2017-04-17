using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Host.SystemWeb;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Identity;
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
            User user = UserManager.FindByEmail(model.Email);
            if (user == null)
            {
                return SignInStatus.Failure;
            }
            else
            {
                return await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
            }           
        }

        public async Task<IdentityResult> Register(RegisterViewModel registerModel)
        {
            var user = new User { UserName = registerModel.Name, Email = registerModel.Email };
            var result = await UserManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded) return result;
            if (registerModel.IsServiceProvider)
            {
                UserManager.AddToRole(user.Id, "service provider");
                DataBase.ServiceProviders.Create(new ServiceProvider() {UserId = user.Id});
                DataBase.Save();            
            }
            else
            {
                UserManager.AddToRole(user.Id, "client");
                DataBase.Clients.Create(new Client() {UserId = user.Id});
                DataBase.Save();
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
