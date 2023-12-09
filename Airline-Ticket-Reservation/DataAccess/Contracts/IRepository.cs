using Domain.Contracts;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAccess.Contracts;

/// <summary>
/// Generic repository interface which defines the basic CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The entity that the operations will be performed on</typeparam>
public interface IRepository<TEntity, TKey> where TEntity : IDbModel
{
    bool Add(TEntity entity);
    bool AddRange(IEnumerable<TEntity> entities);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
    TEntity? Get(TKey id);
    IQueryable<TEntity> GetAll();

}