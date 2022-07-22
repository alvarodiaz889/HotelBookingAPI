using HotelBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAPI.Data
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .Property(u => u.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Person>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Person> People { get; set; }

        public DbSet<Room> Rooms { get; set; }
    }
}
