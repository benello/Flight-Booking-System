using Application.Contracts;
using DataAccess.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class PassportService
    : IPassportService
{
    private readonly IRepository<Passport> passportRepo;
    
    public PassportService(IRepository<Passport> passportRepo)
    {
        this.passportRepo = passportRepo;
    }
    
    public bool PassportExists(string passportNumber)
    {
        return passportRepo.GetAll().Any(ticket => ticket.PassportNumber == passportNumber);
    }
    
    public void AddPassport(Passport passport)
    {
        var passportAdded = passportRepo.Add(passport);
        if (!passportAdded)
            throw new DbUpdateException("Passport could not be added.");
    }
}