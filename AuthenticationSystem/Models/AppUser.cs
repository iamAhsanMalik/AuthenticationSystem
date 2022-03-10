using Microsoft.AspNetCore.Identity;

namespace AuthenticationSystem.Models
{
    public class AppUser : IdentityUser
    {
        public int MyProperty { get; set; }
    }
}
