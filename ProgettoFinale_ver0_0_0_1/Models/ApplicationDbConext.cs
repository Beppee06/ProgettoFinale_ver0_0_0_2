using Microsoft.EntityFrameworkCore;
using ProgettoFinale_ver0_0_0_1.Models.Users;
using ProgettoFinale_ver0_0_0_1.Models.Orders;
using ProgettoFinale_ver0_0_0_1.Models.Books;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }
}