using AppFinaleLibri.Models;

namespace ProgettoFinale_ver0_0_0_1.Managers.Interfaces
{
    public interface IBookManager
    {
        Task<IEnumerable<Book>> GetBookList();
        Task CreateBook(SimpleBook book);
        Task<IEnumerable<Book>> GetBookListFiltered(SimpleBook book);
        Task<bool> checkFilteringFields(SimpleBook b);
    }
}
