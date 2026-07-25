using AlphaCarDetailing.Booking.Domain.Bookings;

namespace AlphaCarDetailing.Booking.Application.Bookings
{
    using Booking = AlphaCarDetailing.Booking.Domain.Bookings.Booking;

    public interface IBookingRepository
    {
        Task AddAsync(Booking booking, CancellationToken cancellationToken);
        Task<Booking?> GetByIdAsync(BookingId id, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
