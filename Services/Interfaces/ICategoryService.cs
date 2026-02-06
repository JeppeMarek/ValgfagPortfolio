using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Services;

public interface ICategoryService
{
    Task<bool> CreateCategoryAsync(Category category);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Task<bool> UpdateCategoryAsync(Category category);
    Task<bool> DeleteCategoryAsync(Category category);
    Task<List<Category>> GetSubCategoriesAsync(int id);
}