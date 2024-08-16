using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.Common;

namespace WebApi.Models;

public class Trip
{
     public long Id { get; set; }
     public string Images { get; set; } = default!;

     [Column("name", TypeName = "jsonb")]
     public MultiLanguageField Name { get; set; } = default!;

     [Column("country", TypeName = "jsonb")]
     public MultiLanguageField Country { get; set; } = default!;

     [Range(0, 5)]
     public float Rank { get; set; }

     public decimal Price { get; set; }
     public decimal Discount { get; set; }

     public DateTime CreatedAt { get; set; }
     public DateTime UpdatedAt { get; set; }
}