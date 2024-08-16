using WebApi.Attributes;

namespace WebApi.Contracts;

public class SignUserDto
{
     public string? Name { get; set; }

     [LocalPhone]
     public string PhoneNumber { get; set; } = default!;
}