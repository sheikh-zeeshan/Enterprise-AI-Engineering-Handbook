namespace AlphaCarDetailing.Booking.Application.Bookings;

public interface IBookingService
{
    Task<BookingDto> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken);
    Task<BookingDto?> GetAsync(Guid bookingId, CancellationToken cancellationToken);
}
