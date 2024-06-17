using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace StellarisAPI.Models;

public class TestModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
