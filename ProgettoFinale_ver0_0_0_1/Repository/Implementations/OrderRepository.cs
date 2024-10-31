using ProgettoFinale_ver0_0_0_1.Models.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoFinale_ver0_0_0_1.Repositories.Interfaces;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;

namespace ProgettoFinale_ver0_0_0_1.Repository.Implementations
{

    internal class OrderRepository : IOrderRepository
    {

        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>?> GetOrders(Guid Id)
        {
            var sol = await _context.Orders.Where(x => x.UserId == Id)
                    .ToListAsync();
            return sol;
        }


        public async Task AddOrder(Guid bookId, Guid UserId)
        {
            await _context.Orders.AddAsync(new Order(UserId, bookId));
            await _context.SaveChangesAsync();
        }
    }
}
