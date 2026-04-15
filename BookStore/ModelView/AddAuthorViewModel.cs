using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ModelView
{
    public class AddAuthorViewModel
    {
        [Display(Name ="Author_Name")]
        [Required]
        public string Name { get; set; }

    }

}
