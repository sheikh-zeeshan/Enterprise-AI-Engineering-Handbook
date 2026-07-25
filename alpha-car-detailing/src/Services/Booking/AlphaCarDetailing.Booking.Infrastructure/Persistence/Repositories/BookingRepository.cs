using AlphaCarDetailing.Booking.Application.Bookings;
using AlphaCarDetailing.Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore;

namespace AlphaCarDetailing.Booking.Infrastructure.Persistence.Repositories
{
    using Booking = AlphaCarDetailing.Booking.Domain.Bookings.Booking;

    public sealed class BookingRepository(BookingDbContext dbContext) : IBookingRepository
    {
        public Task AddAsync(Booking booking, CancellationToken cancellationToken) =>
            dbContext.Bookings.AddAsync(booking, cancellationToken).AsTask();

        public Task<Booking?> GetByIdAsync(BookingId id, CancellationToken cancellationToken) =>
            dbContext.Bookings.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        public Task SaveChangesAsync(CancellationToken cancellationToken) =>
            dbContext.SaveChangesAsync(cancellationToken);
    }
}
