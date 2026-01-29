using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ValgfagPortfolio.Components.Pages;

public partial class Home : ComponentBase
{
    private List<BreadcrumbItem> breadcrumbItems = new();
    private List<Model.Category> categories = new();

    private async Task GetAllCategories()

    {
        categories = await categoryService.GetAllCategoriesAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetAllCategories();
        breadcrumbItems = new List<BreadcrumbItem>
        {
            new("Home", null, true)
        };
    }
}