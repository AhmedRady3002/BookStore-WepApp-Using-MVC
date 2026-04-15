using BookStore.Custom_validation;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ModelView
{
    public class UserRegisterViewModel
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
