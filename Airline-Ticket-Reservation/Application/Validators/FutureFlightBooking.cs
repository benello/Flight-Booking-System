using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validators;

public class FutureFlightBooking
    : ValidationAttribute
{
    private static string GetErrorMessage() => "Flight has already departed";
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // check if flight has already departed by checking if date is in the past or getting the flight and checking

        if (value is DateTime date && date < DateTime.UtcNow
            || value is int flightId && validationContext.GetService<IFlightService>()?.GetFlight(flightId)?.DepartureDate < DateTime.UtcNow)
            return new ValidationResult(GetErrorMessage());
        
        if (value is not DateTime && value is not int)
            return new ValidationResult("Flight id or date is required");
        
        return ValidationResult.Success;
    }
}