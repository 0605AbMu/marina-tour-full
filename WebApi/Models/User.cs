using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;

[Index(nameof(PhoneNumber), IsUnique = true), Index(nameof(VerificationKey))]
[Table("users")]
public class User
{
     [Column("id")]
     public long Id { get; set; }

     [Column("name")]
     public string Name { get; set; } = default!;

     [Column("phone_number")]
     public string PhoneNumber { get; set; } = default!;

     [Column("verification_code")]
     public string? VerificationCode { get; set; } = default!;

     [Column("verification_key")]
     public Guid VerificationKey { get; set; }

     [Column("role")]
     public string Role { get; set; } = default!;
}