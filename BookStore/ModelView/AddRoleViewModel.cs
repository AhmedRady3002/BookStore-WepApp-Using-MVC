using System.ComponentModel.DataAnnotations;

namespace BookStore.ModelView
{
    public class AddRoleViewModel
    {
        [Display(Name ="Role_Name")]
        [Required(ErrorMessage ="*")]
        public string RoleName { get; set; }
    }
}
