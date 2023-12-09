using Domain.Models;

namespace DataAccess.Contracts;

public interface IPassportRepository
    : IRepository<Passport>
{
    bool PassportExists(string passportNumber);
}