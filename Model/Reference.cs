using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ValgfagPortfolio.Model;

public class Reference
{
    [Key] public int Id { get; set; }
    [Required] public string URL { get; set; }
    [StringLength(50)]
    public string Title { get; set; }
    public string Author { get; set; }
    [StringLength(200)]
    public string Note { get; set; }
    public string Subject { get; set; }
}