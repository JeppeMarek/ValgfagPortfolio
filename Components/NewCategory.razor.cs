using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Components;

public partial class NewCategory : ComponentBase
{
    private string[] errors = { };
    private MudForm form;
    private Category newCategory = new Category();
    private bool success;

    private async Task ResetFormAndValidationAsync()
    {
        await form.ResetAsync();
        form.ResetValidation();
    }

    private async Task ValidateAndSaveAsync()
    {
        try
        {
            if (form.IsValid && !newCategory.Title.IsNullOrEmpty())
            {
                success = await categoryService.CreateNewCategory(newCategory);
                if (success) navigationManager.NavigateTo("/", true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}