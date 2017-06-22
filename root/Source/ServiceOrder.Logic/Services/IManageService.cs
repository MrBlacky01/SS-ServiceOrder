using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ServiceOrder.DataProvider.Identity;
using ServiceOrder.ViewModel.ViewModels.Implementation.AccountViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IManageService : IDisposable
    {
        ServiceOrderSignInManager SignInManager { get; set; }
        ServiceOrderUserManager UserManager { get; set; }

        Task<IdentityResult> ChangePassword(ChangePasswordViewModel model);
        Task<bool> IsEmailConfirmed(string userId);
        Task<string> GenerateEmailConfirmCode(string userId);
        Task<string> SendMessageToConfirmEmail(string userId, string backUrl);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
    }
}
