using ProgettoFinale_ver0_0_0_1.Models.Books;

namespace ProgettoFinale_ver0_0_0_1.Managers.Interfaces
{
    public interface IBookManager
    {
        Task<IEnumerable<Book>> GetBookList();
        Task CreateBook(SimpleBook book);
        Task<IEnumerable<Book>> GetBookListFiltered(SimpleBook book);
    }
}
