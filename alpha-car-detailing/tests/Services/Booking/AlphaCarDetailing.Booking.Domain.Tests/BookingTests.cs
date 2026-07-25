using AlphaCarDetailing.Booking.Domain.Bookings;
using AlphaCarDetailing.Booking.Domain.Bookings.Events;
using AlphaCarDetailing.BuildingBlocks.Domain;
using Xunit;

namespace AlphaCarDetailing.Booking.Domain.Tests
{
    using Booking = AlphaCarDetailing.Booking.Domain.Bookings.Booking;

    public sealed class BookingTests
    {
        private static readonly DateTimeOffset Now = new(2026, 7, 25, 8, 0, 0, TimeSpan.Zero);

        [Fact]
        public void Create_ShouldCreatePendingBookingAndRaiseEvent()
        {
            var booking = Booking.Create(
                Guid.NewGuid(), Guid.NewGuid(), VehicleRegistrationNumber.Create("lea-1234"),
                "exterior-wash", Now.AddDays(1), Now);

            Assert.Equal(BookingStatus.Pending, booking.Status);
            Assert.Equal("LEA-1234", booking.VehicleRegistrationNumber.Value);
            Assert.Single(booking.DomainEvents);
            Assert.IsType<BookingCreatedDomainEvent>(booking.DomainEvents.Single());
        }

        [Fact]
        public void Create_ShouldRejectPastSchedule()
        {
            Assert.Throws<DomainException>(() => Booking.Create(
                Guid.NewGuid(), Guid.NewGuid(), VehicleRegistrationNumber.Create("ABC-1"),
                "wash", Now.AddMinutes(-1), Now));
        }
    }
}
