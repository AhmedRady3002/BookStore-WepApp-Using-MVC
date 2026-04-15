using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ModelView
{
	public class AddCategoryNameViewModel
	{
		[Display(Name ="Category_Name")]
		public string CategoryName { get; set; }
		
	}
}
