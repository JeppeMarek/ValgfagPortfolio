## Hello There
There will be an error in `Program.cs`. As it tries to `EnsureAdminAsync`. You can insert this: 

          public static class AdminSeed
          {
                    public static async Task EnsureAdminAsync(IServiceProvider services)
                    {
                        using var scope = services.CreateScope();
                
                        var userManager = scope.ServiceProvider
                            .GetRequiredService<UserManager<ApplicationUser>>();
                
                        var email = "SOMEADMIN@MAIL.COM"; // CHANGE THIS
                        var password = "$UpeR__Str0ng!P4ssw0rD"; // CHANGE THIS
                
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

#### Database MSSQL
Remember to add a migrations using dotnet ef and update database. 
appsettings.json is gitignored, so you will have to create your own
