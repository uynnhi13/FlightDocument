using Microsoft.AspNetCore.Identity;

namespace AuthAPIService.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
