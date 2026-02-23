using Microsoft.AspNetCore.Components;
using ValgfagPortfolio.Model;

namespace ValgfagPortfolio.Components.TopCategories;

public partial class TopCategory : ComponentBase
{
    private Model.Category? Category;

    private bool isExpanded;
    private List<Post> RecentPosts = new();

    private List<Model.Category> SubCategories = new();
    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Category = await CategoryService.GetCategoryByIdAsync(Id);

        SubCategories = await CategoryService.GetSubCategoriesAsync(Id);
        await GetRecentPostsAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        SubCategories = await CategoryService.GetSubCategoriesAsync(Id);
        await GetRecentPostsAsync();
    }

    private async Task GetRecentPostsAsync()
    {
        RecentPosts.Clear();
        foreach (var subCategory in SubCategories)
        {
            foreach (var subCategoryPost in subCategory.Posts)
                RecentPosts.Add(subCategoryPost);
        }
        RecentPosts = RecentPosts.OrderByDescending(post => post.DateEdited).ToList();
    }

    private void OnExpandCollapseClick()
    {
        isExpanded = !isExpanded;
    }
}