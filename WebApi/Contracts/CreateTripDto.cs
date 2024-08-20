using System.ComponentModel.DataAnnotations;
using WebApi.Models.Common;

namespace WebApi.Contracts;

public class CreateTripDto
{
     public string Images { get; set; } = default!;

     public MultiLanguageField Name { get; set; } = default!;

     public MultiLanguageField Country { get; set; } = default!;
     public MultiLanguageField Description { get; set; } = default!;

     [Range(0.001, Int32.MaxValue)]
     public decimal Price { get; set; }

     [Range(0, Int32.MaxValue)]
     public decimal Discount { get; set; }
}