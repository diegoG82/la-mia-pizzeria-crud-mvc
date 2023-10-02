using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.ValidationAttributes
{
    public class MoreThanFive : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string)
            {
                string inputValue = (string)value;

                if (inputValue == null || inputValue.Split(' ').Length <= 5)
                {
                    return new ValidationResult("The field must contain more than five words!");
                }
                return ValidationResult.Success;

            }

            return new ValidationResult("The field is not a string");
        }
    }
}
