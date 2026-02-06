using ValgfagPortfolio.Data;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Persistence.Repositories;

public class ReferenceRepository : IRepository<Reference>
{
    private readonly ApplicationDbContext _context;

    public ReferenceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateEntityAsync(Reference entity)
    {
        await _context.MyReferences.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Reference>> GetAllEntitiesAsync()
    {
        var references = _context.MyReferences.ToList();
        return await Task.FromResult(references);
    }

    public async Task<Reference> GetEntityByIdAsync(int id)
    {
        var reference = _context.MyReferences.FirstOrDefault(r => r.Id == id);
        return reference;
    }

    public async Task UpdateEntityAsync(Reference entity)
    {
        var existingReference = _context.MyReferences.FirstOrDefault(r => r.Id == entity.Id);
        if (existingReference != null)
        {
            _context.Entry(existingReference).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteEntityAsync(Reference entity)
    {
        _context.MyReferences.Remove(entity);
        await _context.SaveChangesAsync();
    }
}