using ProgettoFinale_ver0_0_0_1.Models.Books;
using Microsoft.EntityFrameworkCore;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;

namespace ProgettoFinale_ver0_0_0_1.Repository.Implementations
{
    internal class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Guid?> GetBookId(SimpleBook s)
        {
            var Id = await _context.Books.Where(x=> x.Author == s.Author 
                            && x.Title == s.Title)
                    .Select(x=> x.BookId)
                    .FirstOrDefaultAsync();
            return Id;
        }

        public async Task<Book?> GetBook(SimpleBook s)
        {
            var book = await _context.Books.Where(x => x.Author == s.Author
                            && x.Title == s.Title)
                        .FirstOrDefaultAsync();
            return book;
        }


        public async Task<IEnumerable<Book>> GetBookList()
        {
            var sol = await _context.Books.ToListAsync();
            return sol;
        }


        public async Task<IEnumerable<Book>> GetBookListFiltered(SimpleBook s)
        {
            var sol = await _context.Books.Where(x => 
                    (s.Author != "" 
                        && x.Author.Contains(s.Author))
                    || (s.Title != "" 
                        && x.Title.Contains(s.Title))).ToListAsync();
            return sol;
        }


        public async Task CreateBook(Book c)
        {
            await _context.Books.AddAsync(c);
            await _context.SaveChangesAsync();
        }
    }
}
