﻿using ProgettoFinale_ver0_0_0_1.Models.Users;

namespace ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<User> GetUser(SimpleUser s);
        Task<User> FindUserWithEmail(SimpleUser s);
        Task Register(SimpleUser s);
    }
}
