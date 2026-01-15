using Microsoft.AspNetCore.Identity;

namespace WebAppHotel.Data.Domain
{
    public class AppRole:IdentityRole
    {
        public DateTime DateCreate=DateTime.Now;
    }
}
