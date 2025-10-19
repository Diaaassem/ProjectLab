using System.ComponentModel.DataAnnotations;

namespace ProjectLab.Models.Validators
{
    public class MinDegreeLessThanDegreeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var course = (Course)validationContext.ObjectInstance;
            if (course.MinDegree >= course.Degree)
            {
                return new ValidationResult("MinDegree must be less than Degree.");
            }
            return ValidationResult.Success;
        }
    }
}
