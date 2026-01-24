using Microsoft.AspNetCore.Components;

namespace ValgfagPortfolio.Components.Pages;

public partial class Home : ComponentBase
{
    private List<Model.Category> categories = new();

    private async Task GetAllCategories()

    {
        categories = await categoryService.GetAllCategoriesAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        await GetAllCategories();
    }
}