﻿using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ServiceOrder.DataProvider.Identity;
using ServiceOrder.DataProvider.Interfaces;
using ServiceOrder.ViewModel.ViewModels.Implementation.AccountViewModels;

namespace ServiceOrder.Logic.Services.Implementations
{
    public class ManageService: IManageService
    {
        private IUnitOfWork DataBase { get; set; }

        public ManageService(IUnitOfWork dataBase)
        {
            DataBase = dataBase;
        }

        private ServiceOrderSignInManager _signInManager;
        private ServiceOrderUserManager _userManager;

        public ServiceOrderSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ServiceOrderSignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }
        public ServiceOrderUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ServiceOrderUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);

            var confirmPassword = await UserManager.CheckPasswordAsync(user, model.OldPassword);
            if (!confirmPassword)
            {
                return new IdentityResult("Wrong password");
            }

            return await UserManager.ChangePasswordAsync(user.Id, model.OldPassword, model.NewPassword); ;
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
