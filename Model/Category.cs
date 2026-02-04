using System.ComponentModel.DataAnnotations;

namespace ValgfagPortfolio.Model;

public class Category
{
    [Key] public int Id { get; set; }

    [Required] [StringLength(50)] public string? Title { get; set; }

    [StringLength(1000)] public string? Description { get; set; }

    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public List<Category> Children { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
    public string? CoverImgPath { get; set; }
    public string? LogoImgPath { get; set; }
}