using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
	public class OrderDetails
	{ 
		[ForeignKey("Book")]
        public int BookID  { get; set; }
		public virtual Book Book { get; set; }
		[ForeignKey("Order")]
		public int OrderID { get; set; }
		public virtual Order Order { get; set; }
        public double Price { get; set; }
        public int QuantitySold { get; set; }

    }
}
