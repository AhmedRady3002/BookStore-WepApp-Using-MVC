using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
	public class Order
	{
		public int OrderID { get; set; }
	
        public DateTime OrederDate { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
		[ForeignKey("Customer")]
        public int? CustomerID { get; set; }

        public virtual Customer Customer { get; set; }
		public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}
