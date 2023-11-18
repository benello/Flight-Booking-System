namespace Domain.Contracts;

public interface ITickets
{
    void Book();
    void Cancel();
    void GetTicket();
}