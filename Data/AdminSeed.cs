using Microsoft.AspNetCore.Identity;
using ValgfagPortfolio.Data;

public static class AdminSeed
{
    public static async Task EnsureAdminAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedServices = scope.ServiceProvider;

        var userManager = scopedServices
            .GetRequiredService<UserManager<ApplicationUser>>();
        var config = scopedServices.GetRequiredService<IConfiguration>();

        var email = config["AdminName"];
        var password = config["AdminPassword"];

        var user = await userManager.FindByEmailAsync(email);
        if (user != null) return;

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(user, password);
    }
}