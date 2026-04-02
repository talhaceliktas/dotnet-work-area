using Example_4.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Example_4.Models
{
    public record EventRequest : IValidatableObject
    {
        [Required(ErrorMessage = "{0} alani bos birakilamaz!")]
        [FutureDate]
        public DateTime? StartDate { get; init; }


        public DateTime? EndDate { get; init; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate.HasValue && StartDate.HasValue) {
                if (EndDate.Value < StartDate.Value) {
                    yield return new ValidationResult("EndDate, StartDate'den sonra olmalı");
                }
            }
        }
    }
}
