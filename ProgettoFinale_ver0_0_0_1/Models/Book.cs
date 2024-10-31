using System.ComponentModel.DataAnnotations;

namespace ProgettoFinale_ver0_0_0_1.Models.Books
{
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public Book(){}
        public Book(string title, string author) 
        {
            Title = title;
            Author = author;
        }
    }


    public class SimpleBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
