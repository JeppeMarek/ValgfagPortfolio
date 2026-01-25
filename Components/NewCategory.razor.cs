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
    private bool isValid;
    private string? selectedIcon { get; set; }

    private async Task ResetFormAndValidationAsync()
    {
        await form.ResetAsync();
        form.ResetValidation();
    }

    private async Task ResetRadioIconAsync()
    {
        selectedIcon = null;
    }

    private async Task SetIconAsync()
    {
        switch (selectedIcon)
        {
            case null: newCategory.LogoImgPath = null; break;
            case "Log": newCategory.LogoImgPath = "Icons.Material.Filled.Assignment"; break;
            case "Project": newCategory.LogoImgPath = "Icons.Material.Filled.DynamicFeed"; break;
            case "Diverse": newCategory.LogoImgPath = "Icons.Material.Filled.Assignment"; break;
        }
    }

    private async Task<bool> SetCoverImageAsync()
    {
        var isUploaded = false;
        if (coverImage is not null)
            newCategory.CoverImgPath =
                await imageService.UploadImageAsync(coverImage,
                    "categories");
        else
            newCategory.CoverImgPath = "wwwroot/Images/cover/default-cover.jpeg";
        isUploaded = true;
        return isUploaded;
    }

    private void OnCoverImageSelected(InputFileChangeEventArgs e)
    {
        coverImage = e.File;
    }

    private async Task ValidateAndSaveAsync()
    {
        await form.Validate();

        if (!form.IsValid)
            return;

        try
        {
            var isCoverUploaded = await SetCoverImageAsync();
            if (isCoverUploaded)
                await SetIconAsync();
            isValid = await categoryService.CreateCategoryAsync(newCategory);
            if (isValid)
                navigationManager.NavigateTo("/", true);
        }
        catch (Exception ex)
        {
            errors = [ex.Message];
        }
    }
}