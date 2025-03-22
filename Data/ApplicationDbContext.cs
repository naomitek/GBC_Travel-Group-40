using GBC_Travel_Group_40.Models;
using Microsoft.EntityFrameworkCore;
namespace GBC_Travel_Group_40.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Flights> Flights { get; set; }
        public DbSet<Passengers> Passengers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<BookingFlights> BookingFlights { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<CarBookings> CarBookings { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomBookings> RoomBookings { get; set; }
        public DbSet<Cart> Carts { get; set; }
    } 
} 
