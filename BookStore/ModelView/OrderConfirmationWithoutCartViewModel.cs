namespace BookStore.ModelView
{
    public class OrderConfirmationWithoutCartViewModel
    {
        public int orderId { get; set; }
        public string bookName { get; set; }
        public string imageUrl { get; set; }
        public int quantity { get; set; }
        public double totalprice { get; set; }
    }
}
