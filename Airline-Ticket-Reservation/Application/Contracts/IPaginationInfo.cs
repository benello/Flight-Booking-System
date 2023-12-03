namespace Application.Contracts;

public interface IPaginationInfo
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; }
    public int NextPage();
    public int PreviousPage();
}