using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookStore.Custom_validation;

namespace BookStore.ModelView
{
	public class AddBookViewModel
	{
		[Display(Name ="Book_Code")]
		[UniqueBookID(ErrorMessage ="this book is alredy exists enter anouther code")]
		public int Id { get; set; }

		[Display(Name ="Book_Name")]
        [Required(ErrorMessage = "Book Name is required")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public double Price { get; set; }
		[Display(Name ="Quantity")]
		public int AvailableQuantity { get; set; }

        [Required(ErrorMessage = "Select at least one author")]
        [Display(Name = "Authors")]
        public List<int> AuthorIds { get; set; } = new List<int>();

        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }
		[Display(Name ="publication_Date")]
		public DateOnly? date {  get; set; }	
		public string? imageURL { get; set; }

    }
}
