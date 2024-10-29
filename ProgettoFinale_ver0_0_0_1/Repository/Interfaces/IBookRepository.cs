using AppFinaleLibri.Models;

namespace ProgettoFinale_ver0_0_0_1.Repository.Interfaces
{
    public interface IBookRepository
    {
        Task<Guid> GetBookId(SimpleBook s);
        Task<Book> GetBook(SimpleBook s);
        Task<IEnumerable<Book>> GetBookList();
        Task<IEnumerable<Book>> GetBookListFiltered(SimpleBook s);
        Task CreateBook(Book c);
    }
}
