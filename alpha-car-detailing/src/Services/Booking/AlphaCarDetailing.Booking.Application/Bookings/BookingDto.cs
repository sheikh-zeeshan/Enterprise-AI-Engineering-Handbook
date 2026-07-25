namespace AlphaCarDetailing.Booking.Application.Bookings;

public sealed record BookingDto(
    Guid Id,
    Guid CustomerId,
    Guid StationId,
    string VehicleRegistrationNumber,
    string ServiceCode,
    DateTimeOffset ScheduledAtUtc,
    string Status,
    DateTimeOffset CreatedAtUtc);
