using AppFinaleLibri.Models;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;

namespace ProgettoFinale_ver0_0_0_1.Managers.Implementations
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetBookList()
        {
            var sol = await _bookRepository.GetBookList();
            if (sol == null)
                throw new Exception("books do not exist in this database");
            return sol;
        }



        public async Task<bool> checkFilteringFields(SimpleBook b)
        {
            if (!(string.IsNullOrEmpty(b.Author) || string.IsNullOrEmpty(b.Title)))
                return true;
            return false;
        }

        



        public async Task<IEnumerable<Book>> GetBookListFiltered(SimpleBook book)
        {

            var sol = await _bookRepository.GetBookListFiltered(book);
            if (!(string.IsNullOrEmpty(book.Author) || string.IsNullOrEmpty(book.Title))) { 
                var b = new List<Book> { await _bookRepository.GetBook(book) };
                if (b == null)
                    throw new Exception("nessun libro soddisfa la ricerca");
                return b;
            }
            if (sol == null)
                throw new Exception("nessun libro soddisfa la ricerca");
            return sol;
        }



        public async Task CreateBook(SimpleBook s)
        {
            Book book = await _bookRepository.GetBook(s);
            if (book != null)
                throw new Exception("this book already exist");
            await _bookRepository.CreateBook(
                new Book (s.Title, s.Author));
        }
    }
}
