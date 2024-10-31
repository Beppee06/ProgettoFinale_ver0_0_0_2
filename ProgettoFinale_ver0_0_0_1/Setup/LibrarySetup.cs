using ProgettoFinale_ver0_0_0_1.Microsoft.Extensions.Configuration.Wrapper;

using ProgettoFinale_ver0_0_0_1.Repository.Implementations.Orders;
using ProgettoFinale_ver0_0_0_1.Repository.Implementations.Books;
using ProgettoFinale_ver0_0_0_1.Repository.Implementations.Users;

using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Books;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Orders;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces.Users;

using ProgettoFinale_ver0_0_0_1.Managers.Interfaces.Users;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces.Orders;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces.Books;

using ProgettoFinale_ver0_0_0_1.Managers.Implementations.Users;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations.Orders;
using ProgettoFinale_ver0_0_0_1.Managers.Implementations.Books;

namespace ProgettoFinale_ver0_0_0_1.Setup
{
    internal static class LibrarySetup
    {
        public static IServiceCollection AddOrders(this IServiceCollection services)
        {
            services.AddScoped<IOrderManager, OrderManager>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IBookManager, BookManager>()
                .AddScoped<IBookRepository, BookRepository>()
                .AddScoped<IUserManager, UserManager>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IWrapperConfiguration, WrapperConfiguration>();
            return services;
        }
    }
}
