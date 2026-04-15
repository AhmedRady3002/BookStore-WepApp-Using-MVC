using System.ComponentModel.DataAnnotations;

namespace BookStore.ModelView
{
    public class UserLoginViewModel
    {
        [Display(Name ="User_Name")]
        [Required(ErrorMessage ="*")]

        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        
        //public string Role { get; set; }

    }
}
