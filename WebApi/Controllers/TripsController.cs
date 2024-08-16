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
[Route("trips")]
[Authorize(Roles.Admin)]
public class TripsController : ControllerBase
{
     private readonly AppDbContext _context;

     public TripsController(AppDbContext context)
     {
          _context = context;
     }

     [HttpGet]
     [Authorize(Roles.Client)]
     public async Task<Wrapper> GetAll()
     {
          var q = _context.Trips.Select(x => new
          {
               x.Id,
               x.Name,
               x.Images,
               Location = x.Country,
               x.Discount,
               x.Rank,
               x.Price,
               x.CreatedAt,
               x.UpdatedAt
          }).OrderBy(x => x.UpdatedAt);

          return (await q.ToListAsync(), await q.CountAsync());
     }

     [HttpGet("{id:long}")]
     [Authorize(Roles.Client)]
     public async Task<Wrapper> GetById(long id)
     {
          return (await _context.Trips.Select(x => new
          {
               x.Id,
               x.Name,
               x.Images,
               Location = x.Country,
               x.Discount,
               x.Rank,
               x.Price,
               x.CreatedAt,
               x.UpdatedAt
          }).FirstOrDefaultAsync(x => x.Id == id), 200);
     }

     [HttpPost]
     public async Task<Wrapper> Create([FromBody] CreateTripDto dto)
     {
          var trip = new Trip();

          trip.Discount = dto.Discount;
          trip.Name = dto.Name;
          trip.Price = dto.Price;
          trip.Images = dto.Images;
          trip.Country = dto.Location;

          _context.Add(trip);

          await _context.SaveChangesAsync();

          return 200;
     }

     [HttpPut]
     public async Task<Wrapper> Update([FromBody] UpdateTripDto dto)
     {
          var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == dto.Id);

          if (trip is null)
               throw new NotFoundException("Trip not found");

          trip.Discount = dto.Discount;
          trip.Name = dto.Name;
          trip.Price = dto.Price;
          trip.Images = dto.Images;
          trip.Country = dto.Location;
          trip.Rank = dto.Rank;

          _context.Update(trip);

          await _context.SaveChangesAsync();

          return 200;
     }

     [HttpDelete("{id:long}")]
     public async Task<Wrapper> Delete(long id)
     {
          var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == id);

          if (trip is null)
               throw new NotFoundException("Trip not found");

          _context.Trips.Remove(trip);

          await _context.SaveChangesAsync();

          return 200;
     }
}