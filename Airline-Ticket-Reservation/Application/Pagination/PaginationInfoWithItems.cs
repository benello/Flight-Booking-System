
using Application.Contracts;

namespace Application.Pagination;

public class PaginationInfoWithItems<T>
    : IPaginationInfo where T : class
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int) Math.Ceiling(totalItems / (double) PageSize);

    private readonly IQueryable<T> itemsQueryable;
    private readonly int totalItems;

    public IEnumerable<T> PageItems
    {
        get
        {
            var itemsOffset = (CurrentPage - 1) * PageSize;
            
            return itemsQueryable
                .Skip(itemsOffset)
                .Take(PageSize)
                .ToArray();
        }
    }
    
    public PaginationInfoWithItems(int pageSize, IQueryable<T> itemsQueryable, int currentPage = 1)
    {
        totalItems = itemsQueryable.Count();
        CurrentPage = currentPage;
        PageSize = pageSize;
        this.itemsQueryable = itemsQueryable;
    }
    
    public int NextPage()
    {
        if (CurrentPage >= TotalPages)
            return CurrentPage;
        
        return ++CurrentPage;
    }

    public int PreviousPage()
    {
        if (CurrentPage <= 1)
            return CurrentPage;

        return --CurrentPage;
    }

    public void GoToPage(int page)
    {
        if (page < 1 || page > TotalPages)
            return;

        CurrentPage = page;
    }
}

public static class PaginationExtensions
{
    public static PaginationInfoWithItems<T> ToPaginationInfo<T>(this IQueryable<T> itemsQueryable, int pageSize, int page = 1)
        where T : class
    {
        return new PaginationInfoWithItems<T>(pageSize, itemsQueryable, page);
    }
}