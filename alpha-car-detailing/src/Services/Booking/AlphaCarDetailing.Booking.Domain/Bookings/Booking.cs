using AlphaCarDetailing.Booking.Domain.Bookings.Events;
using AlphaCarDetailing.BuildingBlocks.Domain;

namespace AlphaCarDetailing.Booking.Domain.Bookings;

public sealed class Booking : AggregateRoot<BookingId>
{
    private Booking(BookingId id) : base(id) { }

    public Guid CustomerId { get; private set; }
    public Guid StationId { get; private set; }
    public VehicleRegistrationNumber VehicleRegistrationNumber { get; private set; } = null!;
    public string ServiceCode { get; private set; } = string.Empty;
    public DateTimeOffset ScheduledAtUtc { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; }

    public static Booking Create(
        Guid customerId,
        Guid stationId,
        VehicleRegistrationNumber registrationNumber,
        string serviceCode,
        DateTimeOffset scheduledAtUtc,
        DateTimeOffset currentUtc)
    {
        if (customerId == Guid.Empty) throw new DomainException("Customer is required.");
        if (stationId == Guid.Empty) throw new DomainException("Station is required.");
        if (string.IsNullOrWhiteSpace(serviceCode)) throw new DomainException("Service code is required.");
        if (scheduledAtUtc <= currentUtc) throw new DomainException("Booking must be scheduled in the future.");

        var booking = new Booking(BookingId.New())
        {
            CustomerId = customerId,
            StationId = stationId,
            VehicleRegistrationNumber = registrationNumber,
            ServiceCode = serviceCode.Trim().ToUpperInvariant(),
            ScheduledAtUtc = scheduledAtUtc,
            Status = BookingStatus.Pending,
            CreatedAtUtc = currentUtc
        };

        booking.RaiseDomainEvent(new BookingCreatedDomainEvent(
            booking.Id,
            customerId,
            stationId,
            scheduledAtUtc,
            currentUtc));

        return booking;
    }
}
