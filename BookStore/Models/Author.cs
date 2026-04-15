namespace BookStore.Models
{
	public class Author
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual List<AuthorBook> AuthorBooks { get; set; } =new List<AuthorBook>();
    
    }
}
