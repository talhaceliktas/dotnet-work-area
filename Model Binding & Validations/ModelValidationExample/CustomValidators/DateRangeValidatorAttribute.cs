using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ModelValidationExample.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public string OtherPropertyName;

        public DateRangeValidatorAttribute(string otherPropertyName) {
        OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not DateTime toDate)
            {
                if(!DateTime.TryParse(value?.ToString(), out toDate))
                {
                    return new ValidationResult("You must enter a valid date format for the end date!");
                }
            }

            PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

            if (otherProperty == null)
            {
                return new ValidationResult($"Software Error: A field named ‘{OtherPropertyName}’ was not found in the form!");
            }

            var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance);

            DateTime fromDate = otherPropertyValue == null ? DateTime.Now : Convert.ToDateTime(otherPropertyValue);

            if (fromDate > toDate)
            {
                return new ValidationResult(ErrorMessage ?? "The end date cannot be earlier than the start date!");
            }

            return ValidationResult.Success;

        }

    }
}
