using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }

        public int AvailableQuantity { get; set; }
        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        public string? ImageURL { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        public virtual Category Category { get; set; }

        public virtual List<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();

        [Timestamp]
        public byte[] RowVersion { get; set; }





    }
}
