using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelValidationExample.Models
{
    public class Person
    {
        [Required(ErrorMessage = "{0} can't be null or empty.")]
        [DisplayName("Personel Name")]
        [StringLength(40, MinimumLength = 5, ErrorMessage = "{0} should be between {2} and {1} characters long.")]
        [RegularExpression(@"^[\w. ]+$", ErrorMessage = "{0} should contain only alphabets, space and dot (.)")]
        public string? PersonName { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

        [Range(0, 999.99, ErrorMessage = "{0} should be between {1} and {2}")]
        public double? Price { get; set; }

        public override string ToString()
        {
            return $"Person Object - Person Name: {PersonName}" +
                   $"Email {Email}" +
                   $"Email {Phone}" +
                   $"Email {Password}" +
                   $"Email {ConfirmPassword}" +
                   $"Email {Price}";


        }

    }
}
