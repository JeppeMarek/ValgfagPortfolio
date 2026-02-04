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

        RecentPosts = await PostService.GetAllPostsAsync();
        RecentPosts = RecentPosts.OrderBy(post => post.DateEdited).ToList();
    }

    private void OnExpandCollapseClick()
    {
        isExpanded = !isExpanded;
    }
}