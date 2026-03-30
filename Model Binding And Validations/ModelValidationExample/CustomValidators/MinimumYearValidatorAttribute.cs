using System;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        public int MinimumYear { get; set; } = 2000;

        private const string DefaultErrorMessage = "The minimum allowed year for {0} must be {1} or greater.";

        public MinimumYearValidatorAttribute() { }

        public MinimumYearValidatorAttribute(int minimumYear)
        {
            MinimumYear = minimumYear;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is DateTime date)
            {
                if (date.Year < MinimumYear)
                {
                    string messageTemplate = ErrorMessage ?? DefaultErrorMessage;
                    string formattedMessage = string.Format(messageTemplate, validationContext.DisplayName, MinimumYear);

                    return new ValidationResult(formattedMessage);
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("MinimumYearValidator is only available for DateTime types.");
        }
    }
}