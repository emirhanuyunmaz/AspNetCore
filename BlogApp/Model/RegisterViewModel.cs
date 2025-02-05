using System.ComponentModel.DataAnnotations;

namespace BlogApp.Model
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="Max 10 karakter belirtiniz.")]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Parola Tekrarı")]
        [Compare(nameof(Password),ErrorMessage ="Parolanız eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }

    }
}