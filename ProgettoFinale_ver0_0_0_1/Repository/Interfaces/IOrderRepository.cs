using AppFinaleLibri.Models;

namespace ProgettoFinale_ver0_0_0_1.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders(Guid Id);
        Task AddOrder(Guid bookId, Guid UserId);
    }
}
