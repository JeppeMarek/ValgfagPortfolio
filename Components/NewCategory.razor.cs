using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Components;

public partial class NewCategory : ComponentBase
{
    private readonly Category newCategory = new();
    private IBrowserFile? coverImage;
    private string? coverPreviewURL;
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

    private void SetIcon()
    {
        newCategory.LogoImgPath = selectedIcon switch
        {
            "Log" => "/Images/icons/log.png",
            "Project" => "/Images/icons/project.png",
            "Diverse" => "/Images/icons/diverse.png",
            _ => "/Images/icons/default-icon.png"
        };
    }

    private async Task<bool> SetCoverImageAsync()
    {
        var isUploaded = false;
        if (coverImage is not null)
            newCategory.CoverImgPath =
                await imageService.UploadImageAsync(coverImage,
                    "categories");
        else
            newCategory.CoverImgPath = "Images/cover/default-cover.jpeg";
        isUploaded = true;
        return isUploaded;
    }

    private async Task OnCoverImageSelected(InputFileChangeEventArgs e)
    {
        coverImage = e.File;

        using var stream = coverImage.OpenReadStream(5_000_000);
        var buffer = new byte[coverImage.Size];
        await stream.ReadAsync(buffer);

        coverPreviewURL =
            $"data:{coverImage.ContentType};base64,{Convert.ToBase64String(buffer)}";
    }

    private async Task ValidateAndSaveAsync()
    {
        await form.Validate();

        if (!form.IsValid)
            return;

        try
        {
            var isCoverUploaded = await SetCoverImageAsync();
            if (isCoverUploaded) SetIcon();
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