using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Components;

public partial class NewCategory : ComponentBase
{
    private readonly Category newCategory = new();
    private IBrowserFile? coverImage;
    private string[] errors = { };
    private MudForm form;
    private bool success;

    private async Task ResetFormAndValidationAsync()
    {
        await form.ResetAsync();
        form.ResetValidation();
    }

    private async Task ValidateAndSaveAsync()
    {
        await form.Validate();

        if (!form.IsValid)
            return;

        try
        {
            if (coverImage is not null)
                newCategory.CoverImgPath =
                    await imageService.UploadImageAsync(coverImage,
                        "categories");

            success = await categoryService.CreateCategoryAsync(newCategory);

            if (success)
                navigationManager.NavigateTo("/", true);
        }
        catch (Exception ex)
        {
            errors = [ex.Message];
        }
    }

    private void OnCoverImageSelected(InputFileChangeEventArgs e)
    {
        coverImage = e.File;
    }
}