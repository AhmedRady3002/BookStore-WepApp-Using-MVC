using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Custom_validation
{
    public class UniqueUserPhoneAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var db = (BookStoreContext)validationContext
                        .GetService(typeof(BookStoreContext));

            if (db == null)
                throw new Exception("DbContext is null");

            string NewValue = value.ToString();
          var result=  db.Users.SingleOrDefault(u=>u.PhoneNumber == NewValue);
            if (result != null)
            {
                return new ValidationResult($"{ErrorMessage}");
            }
            else
                return ValidationResult.Success;

        }
    }
}
