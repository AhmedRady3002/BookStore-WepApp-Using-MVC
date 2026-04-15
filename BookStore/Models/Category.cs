namespace BookStore.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual HashSet<Book> Books { get; set; } = new HashSet<Book>();
	}
}
