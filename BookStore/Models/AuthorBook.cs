using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
	
	public class AuthorBook
	{
		[ForeignKey("Book")]
        public int Bookcode { get; set; }
		public virtual Book Book { get; set; } 
		[ForeignKey("Author")]
        public int Author_no { get; set; }
		public virtual Author Author { get; set; } 
        public DateOnly? DateOfPublish { get; set; }

    }

}
