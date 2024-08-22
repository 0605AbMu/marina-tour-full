using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Common;

namespace WebApi.Models;

[Table("orders")]
public class Order
{
     [Column("id")]
     public long Id { get; set; }

     [ForeignKey(nameof(User))]
     [Column("user_id")]
     public long UserId { get; set; }

     public User User { get; set; } = default!;

     [ForeignKey(nameof(Trip))]
     [Column("trip_id")]
     public long TripId { get; set; }

     public Trip Trip { get; set; } = default!;

     [Column("quantity")]
     public int Quantity { get; set; }

     [Column("fee")]
     public decimal Fee { get; set; }

     [Column("created_at")]
     public DateTime CreatedAt { get; set; }

     [Column("updated_at")]
     public DateTime UpdatedAt { get; set; }

     [Column("status")]
     public EnumStatusOrder Status { get; set; }
}