using ProgettoFinale_ver0_0_0_1.Managers.Implementations;
using ProgettoFinale_ver0_0_0_1.Managers.Interfaces;
using ProgettoFinale_ver0_0_0_1.Microsoft.Extensions.Configuration.Wrapper;
using ProgettoFinale_ver0_0_0_1.Repositories.Implementations;
using ProgettoFinale_ver0_0_0_1.Repositories.Interfaces;
using ProgettoFinale_ver0_0_0_1.Repository.Implementations;
using ProgettoFinale_ver0_0_0_1.Repository.Interfaces;

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
