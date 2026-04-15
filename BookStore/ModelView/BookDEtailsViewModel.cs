namespace BookStore.ModelView
{
	public class BookDEtailsViewModel
	{
		    public int bookid { get; set; }
			public  string bookname { get; set; }
	     	public double price { get; set; }
		    public string description { get; set; }
			public int Quantity { get; set; }
			public string ImageURL { get; set; }
			public string Category_Name { get; set; }
			public string Author_Name { get; set; }
			public string Date {  get; set; }	
		    public int QuantityRequired { get; set; }
	}
}
