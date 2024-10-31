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
#pragma warning disable CS8603
        public async Task<User> GetUser(SimpleUser s)
        {
            var sol = await _context.Users.Where(x=> x.Email == s.Email 
                            && x.Password == s.Password)
                        .FirstOrDefaultAsync();
            return sol;
        }



        public async Task<User> FindUserWithEmail(SimpleUser s)
        {
            var sol = await _context.Users.Where(x=> x.Email == s.Email).FirstOrDefaultAsync();
            return sol;
        }
#pragma warning restore CS8603


        public async Task Register(SimpleUser s)
        {
            await _context.AddAsync(new User(s.Email, s.Password));
            await _context.SaveChangesAsync();
        }
    }
}
