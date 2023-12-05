using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Services;

public class TransactionService
{
    private readonly AirlineDbContext dbContext;
    
    public TransactionService(AirlineDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    /// <summary>
    /// Creates a transaction with the database. Needs to be disposed after use.
    /// </summary>
    /// <returns>The db context transaction.</returns>
    public IDbContextTransaction BeginTransaction()
    {
        return dbContext.Database.BeginTransaction();
    }
}