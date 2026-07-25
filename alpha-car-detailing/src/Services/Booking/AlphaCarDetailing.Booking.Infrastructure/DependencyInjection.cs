using AlphaCarDetailing.Booking.Application.Bookings;
using AlphaCarDetailing.Booking.Infrastructure.Persistence;
using AlphaCarDetailing.Booking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlphaCarDetailing.Booking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBookingInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BookingDatabase")
            ?? "Data Source=alpha-car-detailing.db";

        services.AddDbContext<BookingDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IBookingRepository, BookingRepository>();
        return services;
    }
}
