using Example_4.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Example_4.Models
{
    public record EventRequest
    {
        [Required(ErrorMessage = "{0} alani bos birakilamaz!")]
        [FutureDate]
        public DateTime? StartDate { get; init; }
    }
}
