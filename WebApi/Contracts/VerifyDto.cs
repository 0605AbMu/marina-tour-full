using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts;

public class VerifyDto
{
     public Guid Key { get; set; }

     [Length(maximumLength: 6, minimumLength: 6)]
     public string Code { get; set; } = default!;
}