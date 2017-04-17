using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Identity;
using ServiceOrder.Logic.Services.Implementations;
using ServiceOrder.ViewModel.ViewModels.Implementation.AccountViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IAccountService : IDisposable
    {
        ServiceOrderSignInManager SignInManager { get; set; }
        ServiceOrderUserManager UserManager { get; set; }

        Task<SignInStatus> Login(LoginViewModel model);
        Task<IdentityResult> Register(RegisterViewModel registerModel);
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel model);
        Task SignIn(User user);

    }
}
