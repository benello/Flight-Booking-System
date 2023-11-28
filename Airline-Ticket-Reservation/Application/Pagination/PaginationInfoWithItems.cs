
using Application.Contracts;

namespace Application.Pagination;

public class PaginationInfoWithItems<T>
    : IPaginationInfo where T : class
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    
    public IEnumerable<T> Items { get; set; }
    
    public PaginationInfoWithItems(int page, int pageSize, int totalPages, IEnumerable<T> items)
    {
        Page = page;
        PageSize = pageSize;
        TotalPages = totalPages;
        Items = items;
    }
}

public static class PaginationExtensions
{
    public static PaginationInfoWithItems<T> ToPaginationInfo<T>(this IEnumerable<T> items, int page, int pageSize)
        where T : class
    {
        int totalPages;
        int totalItems;
        IEnumerable<T> paginatedItems;
        page = page > 0 ? page : 1;
                
        if (items is IQueryable<T> queryableItems)
        {
            totalItems = queryableItems.Count();
            totalPages = (int) Math.Ceiling(totalItems / (double) pageSize);
            var itemsToSkip = (int) Math.Min((page - 1) * pageSize, (uint) (totalItems - pageSize));
            paginatedItems = queryableItems
                                .Skip(itemsToSkip)
                                .Take(pageSize);
        }
        else
        {
            var arrayItems = items.ToArray();
            totalItems = arrayItems.Length;
            totalPages = (int) Math.Ceiling(totalItems / (double) pageSize);
            var pagesToSkip = (int) Math.Min((page - 1) * pageSize, (uint)(totalItems - pageSize));
            
            paginatedItems = arrayItems
                                .Skip(pagesToSkip)
                                .Take(pageSize);
        }

        return new PaginationInfoWithItems<T>(page, pageSize, totalPages, paginatedItems);
    }
}