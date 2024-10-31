using System.ComponentModel.DataAnnotations;

namespace ProgettoFinale_ver0_0_0_1.Models.Orders
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public Order(Guid id, Guid bookId)
        {
            Id = Guid.NewGuid();
            UserId = id;
            BookId = bookId;
        }
    }
}
