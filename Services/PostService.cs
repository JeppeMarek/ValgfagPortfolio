using Microsoft.IdentityModel.Tokens;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Services;

public class PostService : IPostService
{
    private readonly IRepository<Post> repository;
    private List<Post> posts = new();

    public PostService(IRepository<Post> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> CreatePostAsync(Post post)
    {
        var success = false;
        if (post == null || post.Title.IsNullOrEmpty())
        {
            success = false;
        }
        else
        {
            await repository.CreateEntityAsync(post);
            success = true;
        }

        return success;
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        posts = await repository.GetAllEntitiesAsync();
        return posts.Count <= 0 ? new List<Post>() : posts;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        if (id <= 0) return null;
        return await repository.GetEntityByIdAsync(id);
    }

    public async Task<bool> UpdatePostAsync(Post post)
    {
        var success = post != null || post.Id != -1;
        if (success) await repository.UpdateEntityAsync(post);
        return success;
    }

    public async Task<bool> DeletePostAsync(Post post)
    {
        var success = post != null || post.Id != -1;
        if (success) await repository.DeleteEntityAsync(post);
        return success;
    }
}