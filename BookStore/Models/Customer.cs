using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
	public class Customer
	{
		[Key]
        public int CustomerID { get; set; }
		public string CustomerName { get; set; }
		public string CustomerPhone { get; set; }
        public string Governate { get; set; }
		public string Markaz { get; set; }
		public string Village { get; set; }
		public virtual List<Order> Orders { get; set; }=new List<Order>();


    }
}
