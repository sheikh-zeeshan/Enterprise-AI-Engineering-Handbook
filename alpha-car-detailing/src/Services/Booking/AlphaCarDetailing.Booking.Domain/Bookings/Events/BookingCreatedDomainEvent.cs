using AlphaCarDetailing.BuildingBlocks.Domain;

namespace AlphaCarDetailing.Booking.Domain.Bookings.Events;

public sealed record BookingCreatedDomainEvent(
    BookingId BookingId,
    Guid CustomerId,
    Guid StationId,
    DateTimeOffset ScheduledAtUtc,
    DateTimeOffset OccurredOnUtc) : IDomainEvent;
