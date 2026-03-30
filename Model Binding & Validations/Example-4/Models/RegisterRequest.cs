using System.ComponentModel.DataAnnotations;

namespace Example_4.Models
{
    public record RegisterRequest
    {
        [Required(ErrorMessage = "{0} girmeniz zorunludur!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Lutfen {1} ve {2} uzunluklari arasinda metin girin!")]
        [RegularExpression(@"[a-zA-Z0-9_]")]
        string? Username { get; init; }


        [Required]
        [EmailAddress(ErrorMessage = "Lutfen gecerli bir mail adresi giriniz!")]
        string? Email { get; init; }

        [Phone(ErrorMessage = "Lutfen gecerli bir telefon giriniz!")]
        string? Phone { get; init; }

        [Required(ErrorMessage = "Sifre alani bos olamaz!")]
        string? Password { get; init; }

        [Required(ErrorMessage = "Sifre alani bos olamaz!")]
        [Compare(nameof(Password), ErrorMessage = "Lutfen iki sifreyi ayni giriniz")]
        string? PasswordConfirm { get; init; }

        [Range(18, 99, ErrorMessage = "Lutfen {1} - {2} araliginda yas giriniz!")]
        int? Age { get; init; }

        [Url(ErrorMessage = "Lutfen gecerli bir website giriniz!")]
        string? Website {  get; init; }

    }
}
