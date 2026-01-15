using Microsoft.AspNetCore.Identity;
using WebAppHotel.Data.Domain;

namespace WebAppHotel.Data.Seed
{
    public static class AdminSeeder
    {
        public static async Task SeedFirstUserAsAdminAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            // ако вече има потребители -> не правим нищо
            if (userManager.Users.Any())
                return;

            // ако няма потребители, първият който се регистрира няма как да го хванем тук,
            // затова този вариант е за "създавам админ ръчно" (по-долу ще дам по-чистия начин).
        }
    }
}
