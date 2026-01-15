using Microsoft.AspNetCore.Identity;

namespace EventuresApp.Domain
{
    public class EventuresUser:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
