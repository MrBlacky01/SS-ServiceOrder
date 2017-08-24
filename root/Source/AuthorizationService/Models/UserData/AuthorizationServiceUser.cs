using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationService.Models.UserData
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class AuthorizationServiceUser : IdentityUser
    {
        public virtual ICollection<AuthorizationServiceUserRole> Roles { get; } = new List<AuthorizationServiceUserRole>();

        public virtual ICollection<AuthorizationServiceUserClaim> Claims { get; } = new List<AuthorizationServiceUserClaim>();

        public virtual ICollection<AuthorizationServiceUserLogin> Logins { get; } = new List<AuthorizationServiceUserLogin>();
    }
}
