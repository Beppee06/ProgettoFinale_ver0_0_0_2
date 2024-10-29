using AppFinaleLibri.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProgettoFinale_ver0_0_0_1.Repositories.Interfaces
{
    public interface IOrderManager
    {
        Task<IEnumerable<Order>> GetOrders(Guid Id);
        Task CreateOrder(SimpleBook book, Guid UserId);
        Task AddOrder(SimpleBook book, Guid UserId);
        Task<Guid> FoundBook(SimpleBook simpleBook);
    }
}
