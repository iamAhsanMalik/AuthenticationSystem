using Microsoft.AspNetCore.Identity;

namespace AuthenticationSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public bool TermsAgreement { get; set; } = false;
    }
}
