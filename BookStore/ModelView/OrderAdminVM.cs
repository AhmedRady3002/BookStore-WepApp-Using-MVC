namespace BookStore.ModelView
{
    public class OrderAdminVM
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Governate { get; set; }
        public string Markaz { get; set; }
        public string Village { get; set; }

        public string BookName { get; set; }
        public int QuantitySold { get; set; }

        public double TotalPrice { get; set; }
    }
}
