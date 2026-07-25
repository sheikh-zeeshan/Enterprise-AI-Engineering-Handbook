using AlphaCarDetailing.Booking.Domain.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCarDetailing.Booking.Infrastructure.Persistence.Configurations
{
    using Booking = AlphaCarDetailing.Booking.Domain.Bookings.Booking;

    public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasConversion(id => id.Value, value => BookingId.From(value))
                .ValueGeneratedNever();

            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.StationId).IsRequired();
            builder.Property(x => x.ServiceCode).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ScheduledAtUtc).IsRequired();
            builder.Property(x => x.CreatedAtUtc).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(20).IsRequired();

            builder.OwnsOne(x => x.VehicleRegistrationNumber, owned =>
            {
                owned.Property(x => x.Value)
                    .HasColumnName("VehicleRegistrationNumber")
                    .HasMaxLength(20)
                    .IsRequired();
            });

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
