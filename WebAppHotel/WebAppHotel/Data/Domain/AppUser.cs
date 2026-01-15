using Microsoft.AspNetCore.Identity;

namespace WebAppHotel.Data.Domain
{
    public class AppUser:IdentityUser
    {
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
