using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResultWrapper.Library;
using WebApi.Brokers;
using WebApi.Common;
using WebApi.Contracts;
using WebApi.Exceptions;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("orders")]
[Authorize(Roles.Client)]
public class OrderController : AuthorizedControllerBase
{
     private readonly AppDbContext _context;

     public OrderController(AppDbContext context)
     {
          _context = context;
     }

     [HttpGet("all")]
     [Authorize(Roles.Admin)]
     public async Task<Wrapper> GetAllOrder()
     {
          var q = _context.Orders.Select(x => new
          {
               x.Id,
               x.CreatedAt,
               x.UpdatedAt,
               User = new
               {
                    x.User.Id,
                    x.User.Name,
                    x.User.PhoneNumber
               },
               Trip = new
               {
                    x.Trip.Id,
                    x.Trip.Name,
                    x.Trip.Price,
                    x.Trip.Discount
               },
               x.Fee,
               x.Quantity,
          });

          return (await q.ToListAsync(), await q.CountAsync());
     }

     [HttpGet]
     public async Task<Wrapper> GetUserOrders()
     {
          var q = _context.Orders
               .Where(x => x.UserId == this.UserId)
               .Select(x => new
               {
                    x.Id,
                    x.CreatedAt,
                    x.UpdatedAt,
                    User = new
                    {
                         x.User.Id,
                         x.User.Name,
                         x.User.PhoneNumber
                    },
                    Trip = new
                    {
                         x.Trip.Id,
                         x.Trip.Name,
                         x.Trip.Price,
                         x.Trip.Discount
                    },
                    x.Fee,
                    x.Quantity,
               });

          return (await q.ToListAsync(), await q.CountAsync());
     }

     [HttpPost]
     public async Task<Wrapper> Create([FromBody] CreateOrderDto dto)
     {
          var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == dto.TripId);

          if (trip is null)
               throw new NotFoundException("Trip not found");

          var fee = (trip.Price - trip.Discount) * dto.Quantity;

          var order = new Order()
          {
               Fee = fee,
               Quantity = dto.Quantity,
               UserId = this.UserId,
               TripId = trip.Id,
               Status = EnumStatusOrder.New
          };

          _context.Orders.Add(order);

          await _context.SaveChangesAsync();

          return 200;
     }
}