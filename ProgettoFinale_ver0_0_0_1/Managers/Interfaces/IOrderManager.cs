using ProgettoFinale_ver0_0_0_1.Models.Orders;
using ProgettoFinale_ver0_0_0_1.Models.Books;
using Microsoft.AspNetCore.Mvc;

namespace ProgettoFinale_ver0_0_0_1.Managers.Interfaces.Orders
{
    public interface IOrderManager
    {
        Task<IEnumerable<Order>> GetOrders(Guid Id);
        Task CreateOrder(SimpleBook book, Guid UserId);
        Task AddOrder(SimpleBook book, Guid UserId);
        Task<Guid> FoundBook(SimpleBook simpleBook);
    }
}
