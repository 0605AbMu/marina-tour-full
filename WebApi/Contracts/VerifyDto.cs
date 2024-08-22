using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts;

public class VerifyDto
{
     public Guid Key { get; set; }

     [MinLength(6), MaxLength(6)]
     public string Code { get; set; } = default!;
}