using System.ComponentModel.DataAnnotations;

namespace Example_4.Attributes
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public readonly string DefaultErrorMessage = "{0} alanı bugünden eski olamaz! (Girilen: {1})";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not DateTime date)
            {
                return new ValidationResult("Lutfen gecerli bir tarih giriniz!");
            }

            if (date < DateTime.Now)
            {
                return new ValidationResult(
                    string.Format(ErrorMessage ?? DefaultErrorMessage,
                    validationContext.DisplayName, value.ToString()));
            }

            return ValidationResult.Success;
        }
    }
}
