using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ValgfagPortfolio.Services;

namespace ValgfagPortfolio.Components.Category;

public partial class NewCategory : ComponentBase
{
    private List<BreadcrumbItem> breadcrumbItems = new();
    
    private readonly Model.Category newCategory = new();
    private int selectedParentCategoryId;
    
    private List<Model.Category> parentCategories = new();
    
    private IBrowserFile? coverImage;
    private string? coverPreviewURL;
    
    private bool isValid;
    private MudForm form;
    private string[] errors = { };
    
    [Inject] private IBlobStorageService BlobStorageService { get; set; }
    [Inject] private ICategoryService CategoryService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    private string? selectedIcon { get; set; }

    protected override async Task OnInitializedAsync()
    {
        breadcrumbItems = new List<BreadcrumbItem>
        {
            new("Home", "/"),
            new("Ny kategori", null, true)
        };
        var allCategories = await CategoryService.GetAllCategoriesAsync();
        parentCategories = allCategories.Where(parent => parent.ParentCategoryId is null).ToList();
        selectedParentCategoryId = newCategory.ParentCategoryId ?? 0;
    }

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
        {
            try
            {
                newCategory.CoverImgPath =
                    await BlobStorageService.UploadImageAsync(coverImage, "categories");
                isUploaded = true;
            }
            catch (Exception ex)
            {
                errors = [ex.Message];
            }
        }
        else
        {
            newCategory.CoverImgPath = "Images/cover/default-cover.jpeg";
            isUploaded = true;
        }

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
            newCategory.ParentCategoryId =
                selectedParentCategoryId == 0 ? null : selectedParentCategoryId;
            var isCoverUploaded = await SetCoverImageAsync();
            if (isCoverUploaded) SetIcon();
            isValid = await CategoryService.CreateCategoryAsync(newCategory);
            if (isValid)
                NavigationManager.NavigateTo("/", true);
        }
        catch (Exception ex)
        {
            errors = [ex.Message];
        }
    }
}
