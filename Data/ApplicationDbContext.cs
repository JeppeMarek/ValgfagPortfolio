using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // Set what tables needed for domain classes
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Category> _Categories { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Create Entities for domain classes
        base.OnModelCreating(builder);
        builder.Entity<Post>().HasKey(p => p.Id);
        builder.Entity<Category>().HasKey(c => c.Id);

        // Seed Categories
        builder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Title = "Test",
                Description = "Det her er noget tekst for at teste hvordan det kommer til at se ud",
                CoverImgPath = "Images/Category-Class.png",
                LogoImgPath = "Images/Logo-lib.png"
            },
            new Category
            {
                Id = 2,
                Title = "Test2",
                Description = "Det her er noget tekst for at teste hvordan det kommer til at se ud",
                CoverImgPath = "Images/Category-Class.png",
                LogoImgPath = "Images/Logo-lib.png"
            }
        );

        // Seed Posts
        builder.Entity<Post>().HasData(
            new Post
            {
                Id = 1,
                Title = "Test Post",
                Content = "Det her er bare noget test indhold, værsgo",
                DateCreated = DateTime.Today,
                DateEdited = DateTime.Today,
                CategoryId = 1
            },
            new Post
            {
                Id = 2,
                Title = "Test Post2",
                Content = "Det her er bare noget2 test indhold2, værsgo2",
                DateCreated = DateTime.Today.AddDays(2),
                DateEdited = DateTime.Today.AddDays(1),
                CategoryId = 1
            }
        );
    }
}