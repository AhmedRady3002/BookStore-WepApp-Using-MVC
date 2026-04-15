using BookStore.Custom_validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ModelView
{
    public class AdminRegisterationViewModel
    {
        [Required]
        [Display(Name = "First_Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last_Name")]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [UniqueUserPhone(ErrorMessage = "Phone already Exists")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm_Password")]
        public string PasswordConfirmed { get; set; }


       

    }
}
