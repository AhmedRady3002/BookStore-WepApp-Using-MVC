using BookStore.Custom_validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ModelView
{
    public class CustomerData
    {
        [Required]
       
        [Display(Name = "الرقم")]
        public string Phone { get; set; }
        [Required]
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Required]
        [Display(Name ="المركز")]
        public string Markaz { get; set; }
        [Required]
        [Display(Name = "المحافظه")]
        public string Governate { get; set; }
        [Required]
        [Display(Name = "القريه")]
        public string Village { get; set; }
         

    }
}
