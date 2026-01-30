using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Build.Framework;
using MudBlazor;

namespace ValgfagPortfolio.Components.Category;

public partial class EditCategory : ComponentBase
{
    private List<BreadcrumbItem> breadcrumbItems = new();
    private IBrowserFile? coverImage;
    private string? coverPreviewURL;
    private string[] errors = { };
    private MudForm form;
    private bool isValid;
    [Parameter] public int Id { get; set; }
    [Required] public Model.Category selectedCategory { get; set; } = new();
    private string? selectedIcon { get; set; }
    private bool isIconModified { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            selectedCategory = await categoryService.GetCategoryByIdAsync(Id);
            if (selectedCategory is null) throw new NullReferenceException("Category not found " + Id);
            breadcrumbItems = new List<BreadcrumbItem>
            {
                new("Home", "/"),
                new(selectedCategory.Title, $"/category/{selectedCategory.Id}"),
                new("Rediger kategori", null, true)
            };
            selectedIcon = selectedCategory.LogoImgPath;
            coverPreviewURL = selectedCategory.CoverImgPath;
            isIconModified = false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task ResetFormAndValidationAsync()
    {
        await form.ResetAsync();
        form.ResetValidation();
    }

    private async Task ResetRadioIconAsync()
    {
        selectedIcon = null;
        isIconModified = true;
        selectedCategory.LogoImgPath = null;
    }

    private async Task OnIconSelectedAsync()
    {
        isIconModified = true;
        selectedCategory.LogoImgPath = selectedIcon switch
        {
            "Log" => "/Images/icons/log.png",
            "Project" => "/Images/icons/project.png",
            "Diverse" => "/Images/icons/diverse.png",
            _ => "Images/icons/default-icon.png"
        };
    }

    private async Task<bool> SetCoverImageAsync()
    {
        var isUploaded = false;

        if (coverImage is not null)
        {
            selectedCategory.CoverImgPath =
                await imageService.UploadImageAsync(coverImage,
                    "categories");
            isUploaded = true;
        }

        return isUploaded;
    }

    private async Task OnCoverImageSelected(InputFileChangeEventArgs e)
    {
        try
        {
            coverImage = e.File;

            using var stream = coverImage.OpenReadStream(5_000_000);
            var buffer = new byte[coverImage.Size];
            await stream.ReadAsync(buffer);

            coverPreviewURL =
                $"data:{coverImage.ContentType};base64,{Convert.ToBase64String(buffer)}";
            StateHasChanged();
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine(ex);
        }
    }

    private async Task ValidateAndSaveAsync()
    {
        await form.Validate();
        if (!form.IsValid)
            return;
        try
        {
            await SetCoverImageAsync();
            isValid = await categoryService.UpdateCategoryAsync(selectedCategory);
            if (isValid)
                navigationManager.NavigateTo($"/category/{Id}", true);
        }
        catch (Exception ex)
        {
            errors = [ex.Message];
        }
    }
}