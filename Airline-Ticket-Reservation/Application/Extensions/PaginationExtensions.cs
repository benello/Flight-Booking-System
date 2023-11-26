// ReSharper disable PossibleUnintendedQueryableAsEnumerable

using Domain.Contracts;

namespace Application.Extensions;

public static class PaginationExtensions
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> items, int page, int pageSize)
        where T: IDbModel
    {
        page = page > 0 ? page : 1;
        
        if (items is IQueryable<T> queryableItems)
        {
            var itemsToSkip = (int) Math.Min((page - 1) * pageSize, (uint)(queryableItems.Count() - pageSize));
            return queryableItems
                .Skip(itemsToSkip)
                .Take(pageSize);
        }
        else
        {
            var arrayItems = items.ToArray();
            var pagesToSkip = (int) Math.Min((page - 1) * pageSize, (uint)(arrayItems.Length - pageSize));
            
            return arrayItems
                .Skip(pagesToSkip)
                .Take(pageSize);
        }
        
        
        
    }
}