using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes;

/// <summary>
/// ASP.NET DataModel Attribute for checking Uz local phone numbers format via RegEx
/// </summary>
public sealed class LocalPhoneAttribute : RegularExpressionAttribute
{
     public LocalPhoneAttribute() : base(
          @"^\+998([- ])?(90|91|93|94|95|98|99|33|97|71|77|20|88|33|75)([- ])?(\d{3})([- ])?(\d{2})([- ])?(\d{2})$")
     {
          this.ErrorMessage = "Wrong Phone number format";
     }
}