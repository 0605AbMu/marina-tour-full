using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts;

public class UploadFileDto
{
     [Required]
     public IFormFile File { get; set; } = default!;
}