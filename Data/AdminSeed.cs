using Microsoft.AspNetCore.Identity;
using ValgfagPortfolio.Data;

public static class AdminSeed
{
    public static async Task EnsureAdminAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        var email = "JeppeAdmin@portfolio.local";
        var password = ".JeppeAdmin1337.";

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