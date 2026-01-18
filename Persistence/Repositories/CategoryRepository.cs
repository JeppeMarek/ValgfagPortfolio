using ValgfagPortfolio.Data;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Persistence.Repositories;

public class CategoryRepository : IRepository<Category>
{
    private readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task CreateEnitityAsync(Category entity)
    {
        if (entity != null)
        {
            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
        }
        else
        {
            return;
        }
    }

    public async Task<List<Category>> GetAllEntitiesAsync()
    {
        List<Category> categories = _context.Categories.ToList();
        return await Task.FromResult(categories);
    }

    public async Task<Category> GetEntityByIdAsync(int id)
    {
        Category category = _context.Categories.Find(id);
        return await Task.FromResult(category);
    }

    public async Task UpdateEnitityAsync(Category entity)
    {
        Category existingCategory = GetEntityByIdAsync(entity.Id).Result;
        if (existingCategory == null) return;
        _context.Entry(existingCategory).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEnitityAsync(Category entity)
    {
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
    }
}