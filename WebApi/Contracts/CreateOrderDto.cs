using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts;

public class CreateOrderDto
{
     public long TripId { get; set; }

     [Range(1, 30, ErrorMessage = "Quantity must be in between 1 and 30")]
     public int Quantity { get; set; }
}