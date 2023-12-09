using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validators;

public class FutureFlight
    : ValidationAttribute
{
    private static string GetErrorMessage() => "Flight has already departed";
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var service = validationContext.GetService<IAirlineService>() 
                      ?? throw new Exception("Service could not be retrieved");
        
        if (value is not int flightId)
            return new ValidationResult("Id must be of type int");
        
        // check if flight has already departed by checking if date is in the past or getting the flight and checking
        if (service.FlightDeparted(flightId))
            return new ValidationResult(GetErrorMessage());
        
        return ValidationResult.Success;
    }
}