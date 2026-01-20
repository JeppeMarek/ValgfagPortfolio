using ValgfagPortfolio.Data;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Persistence.Repositories;

public class PostRepository : IRepository<Post>
{
    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateEnitityAsync(Post entity)
    {
        _context.Posts.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetAllEntitiesAsync()
    {
        var posts = _context.Posts.ToList();
        return await Task.FromResult(posts);
    }

    public async Task<Post> GetEntityByIdAsync(int id)
    {
        Post post =_context.Posts.FirstOrDefault(p => p.Id == id);
        if (post == null) return null;
        return await Task.FromResult(post);
    }

    public async Task UpdateEnitityAsync(Post entity)
    {
        Post existingPost = GetEntityByIdAsync(entity.Id).Result;
        if (existingPost == null) return;
        _context.Entry(existingPost).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEnitityAsync(Post entity)
    {
        _context.Posts.Remove(entity);
        await _context.SaveChangesAsync();
    }
}