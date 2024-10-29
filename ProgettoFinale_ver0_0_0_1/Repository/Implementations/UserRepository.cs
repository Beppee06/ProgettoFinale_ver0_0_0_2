using esDef.Models;
using Microsoft.EntityFrameworkCore;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;

namespace ProgettoFinale_ver0_0_0_1.Repository.Implementations
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(SimpleUser s)
        {
            User sol = await _context.Users.Where(x=> x.Email == s.Email 
                            && x.Password == s.Password)
                        .FirstAsync();
            return sol;
        }



        public async Task<User> FindUserWithEmail(SimpleUser s)
        {
            var sol = await _context.Users.Where(x=> x.Email == s.Email).FirstOrDefaultAsync();
            return sol;
        }



        public async Task Register(SimpleUser s)
        {
            await _context.AddAsync(new User(s.Email, s.Password));
            await _context.SaveChangesAsync();
        }
    }
}
