using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validators;

public class SingleSeatBooking
    : ValidationAttribute
{
    private static string GetErrorMessage() => "Seat is already taken";
    private readonly string flightIdProperty;
    
    public SingleSeatBooking(string flightIdProperty)
    {
        this.flightIdProperty = flightIdProperty;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var service = validationContext.GetService<ISeatService>() ?? throw new Exception("Service not found");
        var flightId =validationContext.ObjectInstance
            .GetType()
            .GetProperty(flightIdProperty)
            ?.GetValue(validationContext.ObjectInstance) ?? throw new Exception("FlightId not found");
        
        if (value is not int seatId)
            return new ValidationResult("Seat is required");
        
        if (!service.IsSeatAvailable(seatId))
            return new ValidationResult(GetErrorMessage());
        
        if (!service.SeatBelongsToFlight(seatId, (int) flightId))
            return new ValidationResult("Seat does not belong to associated flight");
        
        return ValidationResult.Success;
    }
}