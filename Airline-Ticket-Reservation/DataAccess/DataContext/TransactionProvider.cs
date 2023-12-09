using DataAccess.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.DataContext;

public class TransactionProvider
    : ITransaction
{
    private readonly AirlineDbContext context;

    public TransactionProvider(AirlineDbContext context)
    {
        this.context = context;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return context.Database.BeginTransaction();
    }
}