using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorizationService.Models.UserData;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationService.IdentityServer
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<AuthorizationServiceUser> _claimsFactory;
        private readonly UserManager<AuthorizationServiceUser> _userManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<AuthorizationServiceUser> claimsFactory, UserManager<AuthorizationServiceUser> userManager)
        {
            _claimsFactory = claimsFactory;
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context?.Subject?.Identity?.Name;
            if (sub != null)
            {
                var user = await _userManager.FindByNameAsync(sub);
                if (user == null)
                {
                    throw new ArgumentException("");
                }

                var principal = await _claimsFactory.CreateAsync(user);
                var claims = principal.Claims.ToList();

                //Add more claims like this
                //claims.Add(new System.Security.Roles.Claim("MyProfileID", user.Id));

                context.IssuedClaims = claims;
            }
           
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context?.Subject?.Identity?.Name;
            if (sub != null)
            {
                var user = await _userManager.FindByNameAsync(sub);
                context.IsActive = user != null;
            }
            
        }
    }
}
