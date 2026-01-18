using System.ComponentModel.DataAnnotations;

namespace ValgfagPortfolio.Model;

public class Category
{
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? CoverImgPath { get; set; }
    public string? LogoImgPath { get; set; }
    public List<Post>? Posts { get; set; }
    [Key]
    public int Id { get; set; }
}