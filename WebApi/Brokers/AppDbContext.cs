using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.Models;

namespace WebApi.Brokers;

public class AppDbContext : DbContext
{
     public DbSet<User> Users { get; set; }
     public DbSet<Trip> Trips { get; set; }
     public DbSet<Order> Orders { get; set; }

     public AppDbContext()
     {
     }

     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
     {
     }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
          modelBuilder.Entity<User>()
               .HasData(new User()
               {
                    PhoneNumber = "+998939063651",
                    Name = "0605AbMu",
                    Id = 1,
                    Role = Roles.Admin
               });


          modelBuilder.Entity<Trip>().Property(x => x.CreatedAt).ValueGeneratedOnAdd();
          modelBuilder.Entity<Trip>().Property(x => x.UpdatedAt).ValueGeneratedOnAddOrUpdate();

          modelBuilder.Entity<Order>().Property(x => x.CreatedAt).ValueGeneratedOnAdd();
          modelBuilder.Entity<Order>().Property(x => x.UpdatedAt).ValueGeneratedOnAddOrUpdate();


          base.OnModelCreating(modelBuilder);
     }
}