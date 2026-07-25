using AlphaCarDetailing.Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace AlphaCarDetailing.Booking.Infrastructure.Persistence
{
    using Booking = AlphaCarDetailing.Booking.Domain.Bookings.Booking;

    public sealed class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
    {
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);
        }
    }
}
