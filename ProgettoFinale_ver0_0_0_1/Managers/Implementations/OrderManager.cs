using AppFinaleLibri.Models;
using Microsoft.AspNetCore.Mvc;
using ProgettoFinale_ver0_0_0_1.Managers;
using ProgettoFinale_ver0_0_0_1.Repositories.Interfaces;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;

namespace ProgettoFinale_ver0_0_0_1.Repositories.Implementations
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        public OrderManager(IOrderRepository orderRepository, IBookRepository bookRepository)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Order>> GetOrders(Guid Id)
        {
            var sol = await _orderRepository.GetOrders(Id);
            if (sol == null)
                throw new Exception($"no order corrisponds to {Id}");
            return sol;
        }


        public async Task CreateOrder(SimpleBook book, Guid UserId)
        {
            if (book.Title == null || book.Author == null)
            {
                throw new Exception("not all fields have a value");
            }
            try
            {
                await AddOrder(book, UserId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        

        public async Task AddOrder(SimpleBook simpleBook, Guid UserId)
        {
            try
            {
                Guid bookId = await FoundBook(simpleBook);
                _orderRepository.AddOrder(bookId, UserId);
                
            } catch (Exception e) {
                throw new Exception(e.Message);
            }
        }


        public async Task<Guid> FoundBook(SimpleBook simpleBook)
        {
            Guid sol = await _bookRepository.GetBookId(simpleBook);
            if(sol == null)
            {
                throw new Exception("Book not found");
            }
            return sol;
        }
    }
}
