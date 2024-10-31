using Moq;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations.Orders;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Orders;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Books;
using ProgettoFinale_ver0_0_0_1.Models.Orders;
using ProgettoFinale_ver0_0_0_1.Models.Books;
namespace TestProgettoFinaleVer0_0_0_1.Tests.Orders
{
    internal class OrderTest
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock = new (MockBehavior.Strict);
        private readonly Mock<IBookRepository> _bookRepositoryMock = new(MockBehavior.Strict);

        //getOrder

        [Test]
        public void GetOrdersSuccess()
        {
            OrderManager _orderManagerMock = new(_orderRepositoryMock.Object, _bookRepositoryMock.Object);
            Guid id = new();
            List<Order> ExpectedList = new()
            {
                new Order(new Guid(), new Guid()), // Primo oggetto Order
                new Order(new Guid(), new Guid())  // Secondo oggetto Order
            };

            _orderRepositoryMock.Setup(x => x.GetOrders(id)).ReturnsAsync(ExpectedList);

            Assert.DoesNotThrowAsync(async () => await _orderManagerMock.GetOrders(id));
        }



        [Test]
        public void GetOrdersFail()
        {
            OrderManager _orderManagerMock = new(_orderRepositoryMock.Object, _bookRepositoryMock.Object);
            Guid id = new();

            _orderRepositoryMock.Setup(x => x.GetOrders(id)).ReturnsAsync(() => null);

            Assert.ThrowsAsync<Exception>(async () => await _orderManagerMock.GetOrders(id));
        }



        //createOrder (AddOrder)(FoundBook)


        [Test]
        public void CreateOrderSuccess()
        {
            OrderManager _orderManagerMock = new(_orderRepositoryMock.Object, _bookRepositoryMock.Object);
            Guid id = new();
            SimpleBook simpleBook = new()
            {
                Title = "prova",
                Author = "prova"
            };

            _bookRepositoryMock.Setup(x => x.GetBookId(simpleBook)).ReturnsAsync(id);
            _orderRepositoryMock.Setup(x => x.AddOrder(new Guid(), new Guid())).Returns(Task.CompletedTask);

            Assert.DoesNotThrowAsync(async () => await _orderManagerMock.CreateOrder(simpleBook, id));
        }

        [Test]
        public void CreateOrderNotAllFieldsCompiled()
        {
            OrderManager _orderManagerMock = new(_orderRepositoryMock.Object, _bookRepositoryMock.Object);
            Guid id = new();
            SimpleBook simpleBook0 = new()
            {
                Title = "",
                Author = "prova"
            };
            SimpleBook simpleBook1 = new()
            {
                Title = "prova",
                Author = ""
            };

            _bookRepositoryMock.Setup(x => x.GetBookId(simpleBook0)).ReturnsAsync(id);
            _bookRepositoryMock.Setup(x => x.GetBookId(simpleBook1)).ReturnsAsync(id);
            _orderRepositoryMock.Setup(x => x.AddOrder(new Guid(), new Guid())).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _orderManagerMock.CreateOrder(simpleBook0, id));
            Assert.ThrowsAsync<Exception>(async () => await _orderManagerMock.CreateOrder(simpleBook1, id));
        }



        //foundBook

        [Test]
        public async Task FoundBookNotFound()
        {
            OrderManager _orderManagerMock = new(_orderRepositoryMock.Object, _bookRepositoryMock.Object);
            SimpleBook simpleBook = new()
            {
                Title = "prova",
                Author = "prova"
            };

            _bookRepositoryMock.Setup(x => x.GetBookId(simpleBook)).ReturnsAsync(() => null);

            Assert.ThrowsAsync<Exception>(async () => await _orderManagerMock.FoundBook(simpleBook));
        }
    }
}
