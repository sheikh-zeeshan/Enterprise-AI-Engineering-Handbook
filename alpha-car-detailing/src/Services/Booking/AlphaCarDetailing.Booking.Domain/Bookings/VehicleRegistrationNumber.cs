using AlphaCarDetailing.BuildingBlocks.Domain;

namespace AlphaCarDetailing.Booking.Domain.Bookings;

public sealed record VehicleRegistrationNumber
{
    private VehicleRegistrationNumber(string value) => Value = value;

    public string Value { get; }

    public static VehicleRegistrationNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Vehicle registration number is required.");
        }

        var normalized = value.Trim().ToUpperInvariant();
        if (normalized.Length > 20)
        {
            throw new DomainException("Vehicle registration number cannot exceed 20 characters.");
        }

        return new VehicleRegistrationNumber(normalized);
    }

    public override string ToString() => Value;
}
