namespace AlphaCarDetailing.Booking.Application.Bookings;

public sealed record CreateBookingRequest(
    Guid CustomerId,
    Guid StationId,
    string VehicleRegistrationNumber,
    string ServiceCode,
    DateTimeOffset ScheduledAtUtc);
