using System.ComponentModel.DataAnnotations;
using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Validators;

public class SeatAvailable
    : ValidationAttribute
{
    private static string GetErrorMessage() => "Seat is already taken";
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var service = validationContext.GetService<IAirlineService>() 
                      ?? throw new Exception("Service could not be retrieved");
        
        if (value is not int seatId)
            return new ValidationResult("Id must be int");

        if (service.SeatBooked(seatId))
            return new ValidationResult(GetErrorMessage());
        
        return ValidationResult.Success;
    }
}