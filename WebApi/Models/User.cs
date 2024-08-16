using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

[Index(nameof(PhoneNumber), IsUnique = true), Index(nameof(VerificationKey))]
public class User
{
     public long Id { get; set; }
     public string Name { get; set; } = default!;
     public string PhoneNumber { get; set; } = default!;
     public string? VerificationCode { get; set; } = default!;
     public Guid VerificationKey { get; set; }
     public string Role { get; set; } = default!;
}