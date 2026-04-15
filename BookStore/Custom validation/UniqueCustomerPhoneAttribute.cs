using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Custom_validation
{
    public class UniqueCustomerPhoneAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var db = (BookStoreContext)validationContext
                        .GetService(typeof(BookStoreContext));

            if (db == null)
                throw new Exception("DbContext is null");
            string newvalue = value.ToString();
            var customer=db.Customers.FirstOrDefault(c=>c.CustomerPhone==newvalue);
            if (customer != null)
            {
                return new ValidationResult($"{ErrorMessage}");
            }
            else 
            {
                return ValidationResult.Success;
            }

            
        }

    }
}
