namespace BookStore.ModelView
{
    public class ContentOfCart
    {
       public  int bookId {  get; set; }
        public string BookName { get; set; }
        public double Price { get; set; }
        public int Quantity_Required { get; set; }
        public string ImagURL { get; set; }
        public double TotalPrice { get; set; }
       

    }
}
