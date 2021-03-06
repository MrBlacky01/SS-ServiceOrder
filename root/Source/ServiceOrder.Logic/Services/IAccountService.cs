﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ServiceOrder.DataProvider.Entities;
using ServiceOrder.DataProvider.Identity;
using ServiceOrder.ViewModel.ViewModels.Implementation.AccountViewModels;

namespace ServiceOrder.Logic.Services
{
    public interface IAccountService : IDisposable
    {
        ServiceOrderSignInManager SignInManager { get; set; }
        ServiceOrderUserManager UserManager { get; set; }

        Task<SignInStatus> Login(LoginViewModel model);
        Task<IdentityResult> Register(RegisterViewModel registerModel);
        Task SignIn(User user);
        Task<bool> IsEmailConfirmed(string userId);
        Task<string> GenerateEmailConfirmCode(string userId);
        Task<string> GeneratePasswordResetTokenAsync(string userId);
        Task<string> SendMessageToConfirmEmail(string userId, string backUrl);
        Task<string> SendMessageToForgotPasswordEmail(string userId, string backUrl);
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        Task<IdentityResult> ResetPassword(ResetPasswordViewModel model);
        Task<string> GetIdByEmail(string email);

    }
}
