using System.ComponentModel.DataAnnotations;

namespace RemoveBg.Models;

public class Photo
{
    [Required]
    public IFormFile? Image { get; set; }
}