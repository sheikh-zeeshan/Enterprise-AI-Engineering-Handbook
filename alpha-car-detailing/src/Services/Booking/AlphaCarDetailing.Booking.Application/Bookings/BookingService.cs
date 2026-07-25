using AlphaCarDetailing.Booking.Domain.Bookings;

namespace AlphaCarDetailing.Booking.Application.Bookings
{
    using Booking = AlphaCarDetailing.Booking.Domain.Bookings.Booking;

    public sealed class BookingService(IBookingRepository repository, TimeProvider timeProvider) : IBookingService
    {
        public async Task<BookingDto> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = Booking.Create(
                request.CustomerId,
                request.StationId,
                VehicleRegistrationNumber.Create(request.VehicleRegistrationNumber),
                request.ServiceCode,
                request.ScheduledAtUtc,
                timeProvider.GetUtcNow());

            await repository.AddAsync(booking, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Map(booking);
        }

        public async Task<BookingDto?> GetAsync(Guid bookingId, CancellationToken cancellationToken)
        {
            var booking = await repository.GetByIdAsync(BookingId.From(bookingId), cancellationToken);
            return booking is null ? null : Map(booking);
        }

        private static BookingDto Map(Booking booking) => new(
            booking.Id.Value,
            booking.CustomerId,
            booking.StationId,
            booking.VehicleRegistrationNumber.Value,
            booking.ServiceCode,
            booking.ScheduledAtUtc,
            booking.Status.ToString(),
            booking.CreatedAtUtc);
    }
}
