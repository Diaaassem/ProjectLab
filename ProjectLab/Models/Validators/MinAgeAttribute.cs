using System.ComponentModel.DataAnnotations;

namespace ProjectLab.Models.Validators
{
    public class MinAgeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int age)
            {
                if (age < 18)
                {
                    return new ValidationResult("Age must be at least 18.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid age value.");
        }
    }
}
