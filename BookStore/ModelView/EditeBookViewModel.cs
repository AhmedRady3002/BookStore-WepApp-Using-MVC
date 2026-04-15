using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.ModelView
{
    public class EditeBookViewModel
    {
        [Display(Name ="Book_Code")]
        public int BookID { get; set; }
        [Display(Name = "Book_Name")]
        [Required]
        public string BookName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name = "Quantity")]
        public int AvailableQuantity { get; set; }
        public string? ImageURL { get; set; }
        public int? Categoryid { get; set; }
        public  List<int>? authorIDs { get; set; }
        public DateOnly ?date {  get; set; }

    }
}
