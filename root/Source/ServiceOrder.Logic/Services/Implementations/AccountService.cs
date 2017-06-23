using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using Microsoft.AspNet.Identity;
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
            return result;
        }

        public async Task SignIn(User user)
        {
             await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        }

        public async Task<bool> IsEmailConfirmed(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            return user.EmailConfirmed;
        }

        public async Task<string> GenerateEmailConfirmCode(string userId)
        {
            return await UserManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task<string> SendMessageToConfirmEmail(string userId, string backUrl)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var message = makeMessage(user.Email, backUrl);
            try
            {
                await UserManager.SendEmailAsync(user.Id, message.Subject, message.Body);
                return "";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return result;
        }

        public async Task<string> GetIdByEmail(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            return user?.Id;
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

        private IdentityMessage makeMessage(string userEmail, string backUrl)
        {
            var message = new IdentityMessage();
            message.Subject = "Confirm Email";
            message.Destination = userEmail;
            /**/
            message.Body = "Please confirm your account by clicking <a href=\""
               + backUrl + "\">here</a>";
            return message;
        }
    }
}
