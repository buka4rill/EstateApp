using Microsoft.AspNetCore.Identity;

namespace EstateApp.Data.Entities
{
    // This entity represents User
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}