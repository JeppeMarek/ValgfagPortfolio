using Microsoft.EntityFrameworkCore;
using ValgfagPortfolio.Data;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Persistence.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly ApplicationDbContext context;

    public CategoryRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task CreateEntityAsync(Category entity)
    {
        if (entity != null)
        {
            context.Categories.Add(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<List<Category>> GetAllEntitiesAsync()
    {
        return await context.Categories.ToListAsync();
    }


    public async Task<Category> GetEntityByIdAsync(int id)
    {
        var category = await context.Categories.Include(c => c.Posts)
            .FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }

    public async Task UpdateEntityAsync(Category entity)
    {
        var existingCategory = await GetEntityByIdAsync(entity.Id);
        if (existingCategory == null) return;
        context.Entry(existingCategory).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteEntityAsync(Category entity)
    {
        context.Categories.Remove(entity);
        await context.SaveChangesAsync();
    }
}