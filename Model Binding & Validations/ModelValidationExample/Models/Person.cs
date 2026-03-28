using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ModelValidationExample.CustomValidators;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.Models
{
    public class Person : IValidatableObject
    {
        [Required(ErrorMessage = "{0} can't be null or empty.")]
        [DisplayName("Personel Name")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "{0} should be between {2} and {1} characters long.")]
        [RegularExpression(@"^[\w. ]+$", ErrorMessage = "{0} should contain only alphabets, space and dot (.)")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage = "{0} can't be blank.")]
        [EmailAddress(ErrorMessage = "{0} should be a proper email address.")]
        public string? Email { get; set; }

        //[ValidateNever]
        [Phone(ErrorMessage = "{0} should contain 10 digits")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "{0} can't be blank.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} can't be blank.")]
        [Compare("Password", ErrorMessage = "{0} and {1} don't match.")]
        [Display(Name = "Re-enter Password")]
        //[Url]
        public string? ConfirmPassword { get; set; }

        [Range(0, 999.99, ErrorMessage = "{0} should be between {1} and {2}")]
        public double? Price { get; set; }


        [MinimumYearValidator(2005)]
        public DateTime? DateOfBirth { get; set; }

        //[BindNever]
        public DateTime? FromDate { get; set; }


        [DateRangeValidatorAttribute("FromDate", ErrorMessage = "'From Date' should be older than or equal to 'To Date'")]
        public DateTime? ToDate { get; set; }

        public int? Age { get; set; }

        public override string ToString()
        {
            return $"Person Object - Person Name: {PersonName}" +
                   $"Email {Email}" +
                   $"Email {Phone}" +
                   $"Email {Password}" +
                   $"Email {ConfirmPassword}" +
                   $"Email {Price}";


        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!DateOfBirth.HasValue && !Age.HasValue)
            {
                yield return new ValidationResult("Either of Date of Birth or Age must be supplied.", new[] {nameof(Age)});
            }
        }
    }
}
