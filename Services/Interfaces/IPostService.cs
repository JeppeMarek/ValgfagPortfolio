using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Services;

public interface IPostService
{
    Task<bool> CreatePostAsync(Post post);
    Task<List<Post>> GetAllPostsAsync();
    Task<Post> GetPostByIdAsync(int id);
    Task<bool> UpdatePostAsync(Post post);
    Task<bool> DeletePostAsync(Post post);
}