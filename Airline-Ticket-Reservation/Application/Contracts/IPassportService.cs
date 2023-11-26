using Domain.Models;

namespace Application.Contracts;

public interface IPassportService
{
    
    bool PassportExists(string passportNumber);
    void AddPassport(Passport passport);
}