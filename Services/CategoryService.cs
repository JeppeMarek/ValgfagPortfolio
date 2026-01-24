using Microsoft.IdentityModel.Tokens;
using ValgfagPortfolio.Model;
using ValgfagPortfolio.Persistence.Interfaces;

namespace ValgfagPortfolio.Services;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> repository;
    private List<Category> categories = new();

    public CategoryService(IRepository<Category> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> CreateCategoryAsync(Category category)
    {
        var success = false;
        if (category == null || category.Title.IsNullOrEmpty())
        {
            success = false;
        }
        else
        {
            await repository.CreateEntityAsync(category);
            success = true;
        }

        return success;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        categories = await repository.GetAllEntitiesAsync();
        return categories.Count == 0 ? new List<Category>() : categories;
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        if (id <= 0) return null;
        return await repository.GetEntityByIdAsync(id);
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        var success = category != null || category.Id != -1;
        if (success) await repository.UpdateEntityAsync(category);
        return success;
    }

    public async Task<bool> DeleteCategoryAsync(Category category)
    {
        var success = category != null || category.Id != -1;
        if (success) await repository.DeleteEntityAsync(category);
        return success;
    }
}