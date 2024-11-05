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
            expectedList.Add(new Book());

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




        [Test]
        public async Task GetBookListFilteredSucess()
        {
            BookManager _bookManagerMock = new(_bookRepositoryMock.Object);
            List<Book> expectedList = new();
            expectedList.Add(new Book());
            SimpleBook simpleBook = new ();

            _bookRepositoryMock.Setup(x => x.GetBookListFiltered(simpleBook)).ReturnsAsync(expectedList);
            
            Assert.That(await _bookManagerMock.GetBookListFiltered(simpleBook), Is.EqualTo(expectedList));
        }





        [Test]
        public void GetBookListFilteredFail()
        {
            BookManager _bookManagerMock = new(_bookRepositoryMock.Object);
            List<Book> expectedList = new();
            SimpleBook simpleBook = new();

            _bookRepositoryMock.Setup(x => x.GetBookListFiltered(simpleBook)).ReturnsAsync(expectedList);

            Assert.ThrowsAsync<Exception>(async () => await _bookManagerMock.GetBookListFiltered(simpleBook));
        }





        [Test]
        public void CreateBookSuccess()
        {
            BookManager _bookManagerMock = new(_bookRepositoryMock.Object);
            SimpleBook simpleBook = new();
            Book newBook = new()
            {
                Title = "title",
                Author = "Author"
            };

            _bookRepositoryMock.Setup(x => x.GetBook(simpleBook)).ReturnsAsync(() => null);
            _bookRepositoryMock.Setup(x => x.CreateBook(It.IsAny<Book>())).Returns(Task.CompletedTask);

            Assert.DoesNotThrowAsync(async () => await _bookManagerMock.CreateBook(simpleBook));
        }




        [Test]
        public void CreateBookFailsBookExists()
        {
            BookManager _bookManagerMock = new(_bookRepositoryMock.Object);
            SimpleBook simpleBook = new();
            Book newBook = new()
            {
                Title = "title",
                Author = "Author"
            };

            _bookRepositoryMock.Setup(x => x.GetBook(simpleBook)).ReturnsAsync(newBook);
            _bookRepositoryMock.Setup(x => x.CreateBook(It.IsAny<Book>())).Returns(Task.CompletedTask);

            Assert.ThrowsAsync<Exception>(async () => await _bookManagerMock.CreateBook(simpleBook));
        }
    }
}
