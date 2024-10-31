using ProgettoFinale_ver0_0_0_1.Models.Users;

namespace ProgettoFinale_ver0_0_0_1.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetUser(SimpleUser s);
        Task EmailUsed(SimpleUser s);
        Task<string> Register(SimpleUser s);
        Task<string> Login(SimpleUser s);
    }
}
