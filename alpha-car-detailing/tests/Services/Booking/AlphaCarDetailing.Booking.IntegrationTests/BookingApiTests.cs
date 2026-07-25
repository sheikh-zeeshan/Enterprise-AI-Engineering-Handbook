using System.Net;
using System.Net.Http.Json;
using AlphaCarDetailing.Booking.Application.Bookings;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AlphaCarDetailing.Booking.IntegrationTests;

public sealed class BookingApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BookingApiTests(WebApplicationFactory<Program> factory) => _client = factory.CreateClient();

    [Fact]
    public async Task PostBooking_ShouldReturnCreated()
    {
        var request = new CreateBookingRequest(
            Guid.NewGuid(), Guid.NewGuid(), "LEA-1234", "EXTERIOR-WASH",
            DateTimeOffset.UtcNow.AddDays(1));

        var response = await _client.PostAsJsonAsync("/api/bookings", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var booking = await response.Content.ReadFromJsonAsync<BookingDto>();
        Assert.NotNull(booking);
        Assert.Equal("Pending", booking.Status);
    }
}
