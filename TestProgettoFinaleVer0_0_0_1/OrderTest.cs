using Moq;
using ProgettoFinale_ver0_0_0_1.Repositories.Implementations;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;
using ProgettoFinale_ver0_0_0_1.Models.Orders;

namespace TestProgettoFinaleVer0_0_0_1.Tests.Orders
{
    internal class OrderTest
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new (MockBehavior.Strict);
        private readonly Mock<IBookRepository> _bookRepositoryMock = new(MockBehavior.Strict);

        [Test]
        public async Task GetOrdersSuccess()
        {
            OrderManager _orderManagerMock = new(_orderRepositoryMock.Object, _bookRepositoryMock.Object);
            Guid id = new();
            List<Order> ExpectedList = new()
            {
            };
            _orderRepositoryMock.Setup(x => x.GetOrders(id)).ReturnsAsync(ExpectedList);
            Assert.DoesNotThrowAsync(async () => await _orderManagerMock.GetOrders(id));
        }
    }
}
