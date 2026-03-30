using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace Example_4.Models
{
    public record UserProfile
    {
        public string? Name { get; init; }
        public string? Email { get; init; }

        //[BindNever]
        //[JsonIgnore]
        //public bool IsAdmin { get; init; }

        //[JsonIgnore]
        //[BindNever] public DateTime CreatedAt { get; init; } 

    }
}
