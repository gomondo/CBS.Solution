using Microsoft.AspNetCore.Identity;

namespace CBS.Api.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string FullNames { get; set; } = string.Empty;
    }
}
