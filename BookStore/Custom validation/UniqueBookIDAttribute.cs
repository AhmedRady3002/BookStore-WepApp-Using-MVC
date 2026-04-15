using BookStore.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Custom_validation
{
    public class UniqueBookIDAttribute:ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var db = (BookStoreContext)validationContext
                         .GetService(typeof(BookStoreContext));

            if (db == null)
                throw new Exception("DbContext is null");

            int newValue = (int)value;

            var book = db.Books.FirstOrDefault(b => b.Id == newValue);

            if (book != null)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }


    }

}
