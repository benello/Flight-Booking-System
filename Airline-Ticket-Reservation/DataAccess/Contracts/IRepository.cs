using Domain.Contracts;

namespace DataAccess.Contracts;

/// <summary>
/// Generic repository interface which defines the basic CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The entity that the operations will be performed on</typeparam>
public interface IRepository<TEntity>
    where TEntity : IDbModel
{
    bool Add(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
    TEntity? Get(int id);
    IQueryable<TEntity> GetAll();
}