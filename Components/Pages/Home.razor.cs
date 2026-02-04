using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ValgfagPortfolio.Components.Pages;

public partial class Home : ComponentBase
{
    private List<BreadcrumbItem> breadcrumbItems = new();
    private List<Model.Category> categories = new();
    private List<Model.Category> parentCategories = new();

    private async Task GetAllParentCategories()
    {
        parentCategories = categories.Where(c => c.ParentCategoryId is null).ToList();
    }
    private async Task GetAllCategoriesAsync()
    {
        categories = await categoryService.GetAllCategoriesAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetAllCategoriesAsync();
        await GetAllParentCategories();
        breadcrumbItems = new List<BreadcrumbItem>
        {
            new("Home", null, true)
        };
    }
}