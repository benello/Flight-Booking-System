using Domain.Models;

namespace DataAccess.Contracts;

public interface IPassportRepository
    : IRepository<Passport, string>
{
    bool PassportExists(string passportNumber);
}