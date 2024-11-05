using Moq;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Books;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations.Books;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgettoFinale_ver0_0_0_1.Models.Books;

namespace TestProgettoFinaleVer0_0_0_1.Tests.Books
{
    internal class BookTest
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock = new (MockBehavior.Strict);

        //getBookList

        [Test]
        public async Task GetBookListSuccess()
        {
            BookManager _bookManagerMock = new (_bookRepositoryMock.Object);
            List<Book> expectedList = new ();
            Book book = new Book();
            expectedList.Add(book);

            _bookRepositoryMock.Setup(x => x.GetBookList()).ReturnsAsync(expectedList);

            Assert.That(await _bookManagerMock.GetBookList(), Is.EqualTo(expectedList));
        }


        [Test]
        public void GetBookListFail()
        {
            BookManager _bookManagerMock = new(_bookRepositoryMock.Object);
            List<Book> expectedList = new();

            _bookRepositoryMock.Setup(x => x.GetBookList()).ReturnsAsync(expectedList);

            Assert.ThrowsAsync<Exception>(async () => await _bookManagerMock.GetBookList());
        }
    }
}
