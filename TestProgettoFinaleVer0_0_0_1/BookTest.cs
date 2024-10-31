using Moq;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Books;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations.Books;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProgettoFinaleVer0_0_0_1
{
    internal class BookTest
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock = new (MockBehavior.Strict);


        [Test]
        public async Task boh()
        {
            BookManager _bookManagerMock = new (_bookRepositoryMock.Object);
        }
    }
}
