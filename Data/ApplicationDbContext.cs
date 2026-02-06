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
    public DbSet<Reference> References { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Create Entities for domain classes
        base.OnModelCreating(builder);
        builder.Entity<Post>().HasKey(p => p.Id);
        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.Children)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Reference>().HasKey(r => r.Id);
        
    }
}
