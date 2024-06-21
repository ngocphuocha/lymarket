using Microsoft.AspNetCore.Identity;

namespace LyMarket.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
