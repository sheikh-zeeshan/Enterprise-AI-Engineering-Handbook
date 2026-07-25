using AlphaCarDetailing.Booking.Application.Bookings;
using AlphaCarDetailing.BuildingBlocks.Domain;

namespace AlphaCarDetailing.Booking.Api.Endpoints;

public static class BookingEndpoints
{
    public static IEndpointRouteBuilder MapBookingEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/bookings").WithTags("Bookings");

        group.MapPost("/", CreateBookingAsync)
            .WithName("CreateBooking")
            .Produces<BookingDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        group.MapGet("/{bookingId:guid}", GetBookingAsync)
            .WithName("GetBooking")
            .Produces<BookingDto>()
            .Produces(StatusCodes.Status404NotFound);

        return endpoints;
    }

    private static async Task<IResult> CreateBookingAsync(
        CreateBookingRequest request,
        IBookingService bookingService,
        CancellationToken cancellationToken)
    {
        try
        {
            var booking = await bookingService.CreateAsync(request, cancellationToken);
            return Results.CreatedAtRoute("GetBooking", new { bookingId = booking.Id }, booking);
        }
        catch (DomainException exception)
        {
            return Results.Problem(
                title: "Booking validation failed",
                detail: exception.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }
    }

    private static async Task<IResult> GetBookingAsync(
        Guid bookingId,
        IBookingService bookingService,
        CancellationToken cancellationToken)
    {
        var booking = await bookingService.GetAsync(bookingId, cancellationToken);
        return booking is null ? Results.NotFound() : Results.Ok(booking);
    }
}
