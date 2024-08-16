using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Common;

namespace WebApi.Models;

public class Order
{
     public long Id { get; set; }

     [ForeignKey(nameof(User))]
     public long UserId { get; set; }

     public User User { get; set; } = default!;

     [ForeignKey(nameof(Trip))]
     public long TripId { get; set; }

     public Trip Trip { get; set; } = default!;
     public int Quantity { get; set; }
     public decimal Fee { get; set; }
     public DateTime CreatedAt { get; set; }
     public DateTime UpdatedAt { get; set; }

     public EnumStatusOrder Status { get; set; }
}