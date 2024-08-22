using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.Common;

namespace WebApi.Models;

[Table("trips")]
public class Trip
{
     [Column("id")]
     public long Id { get; set; }

     [Column("images")]
     public string Images { get; set; } = default!;

     [Column("name", TypeName = "jsonb")]
     public MultiLanguageField Name { get; set; } = default!;

     [Column("country", TypeName = "jsonb")]
     public MultiLanguageField Country { get; set; } = default!;

     [Column("description", TypeName = "jsonb")]
     public MultiLanguageField? Description { get; set; } = default!;

     [Range(0, 5)]
     [Column("rank")]
     public float Rank { get; set; }

     [Column("price")]
     public decimal Price { get; set; }

     [Column("discount")]
     public decimal Discount { get; set; }

     [Column("created_at")]
     public DateTime CreatedAt { get; set; }

     [Column("updated_at")]
     public DateTime UpdatedAt { get; set; }
}