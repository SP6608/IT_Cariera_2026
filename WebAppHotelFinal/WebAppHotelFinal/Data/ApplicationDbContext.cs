using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppHotelFinal.Data.Domain;
using WebAppHotelFinal.Models;

namespace WebAppHotelFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Room>()
              .HasIndex(r => r.NumberRoom)
              .IsUnique();
            // Seed Roles (Admin, Employee)
            builder.Entity<AppRole>().HasData(new AppRole { Id = "ROLE_ADMIN", Name = "Admin", NormalizedName = "ADMIN" });
            builder.Entity<AppRole>().HasData(new AppRole { Id="ROLE_EMPLOYEE",Name="Employee",NormalizedName="EMPLOYEE"});
        }
    }
}
