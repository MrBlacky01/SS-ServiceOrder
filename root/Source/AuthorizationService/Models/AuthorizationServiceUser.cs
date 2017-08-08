using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AuthorizationService.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class AuthorizationServiceUser : IdentityUser
    {
        public string PhotoId { get; set; }
    }
}
