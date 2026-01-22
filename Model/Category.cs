using System.ComponentModel.DataAnnotations;

namespace ValgfagPortfolio.Model;

public class Category
{
    [Required] [StringLength(50)] public string? Title { get; set; }

    [StringLength(200)] public string? Description { get; set; }

    public string? CoverImgPath { get; set; }
    public string? LogoImgPath { get; set; }
    public List<Post> Posts { get; set; } = new();

    [Key] public int Id { get; set; }
}