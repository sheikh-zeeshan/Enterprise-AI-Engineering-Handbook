using AlphaCarDetailing.Booking.Application.Bookings;
using Microsoft.Extensions.DependencyInjection;

namespace AlphaCarDetailing.Booking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddBookingApplication(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IBookingService, BookingService>();
        return services;
    }
}
