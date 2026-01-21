using Microsoft.IdentityModel.Tokens;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Services;

public class CategoryService
{
    private List<Category> categories = new();

    private IRepository<Category> repository;

    public async Task<bool> CreateNewCategory(Category category)
    {
        var success = false;
        if (category == null || category.Title.IsNullOrEmpty())
        {
            success = false;
        }
        else
        {
            await repository.CreateEnitityAsync(category);
            success = true;
        }
        return success;
    }

    public async Task<List<Category>> GetAllCategories()
    {
        categories = await repository.GetAllEntitiesAsync();
        return categories.IsNullOrEmpty() ? new List<Category>() : categories;
    }

    public async Task<Category> GetCategoryById(int id)
    {
        if (id <= -1) return null;
        return await repository.GetEntityByIdAsync(id);
    }

    public async Task<bool> UpdateCategory(Category category)
    {
        var success = category == null || category.Id != -1;
        if (success) await repository.UpdateEnitityAsync(category);
        return success;
    }

    public async Task<bool> DeleteCategory(Category category)
    {
        var success = category == null || category.Id != -1;
        if (success) await repository.DeleteEnitityAsync(category);
        return success;
    }
}