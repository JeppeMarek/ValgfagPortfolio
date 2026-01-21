using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValgfagPortfolio.Model;

public class Post
{
    [Required] public string Title { get; set; }

    public string? Content { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateEdited { get; set; }

    [Key] public int Id { get; set; }

    [ForeignKey(nameof(Category))] public int CategoryId { get; set; }
}