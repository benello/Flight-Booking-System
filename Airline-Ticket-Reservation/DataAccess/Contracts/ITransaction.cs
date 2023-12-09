using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Contracts;

public interface ITransaction
{
    /// <summary>
    /// Creates a transaction with the database. Needs to be disposed after use.
    /// </summary>
    /// <returns>The db context transaction.</returns>
    IDbContextTransaction BeginTransaction();
}