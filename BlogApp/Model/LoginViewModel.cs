using System.ComponentModel.DataAnnotations;

namespace BlogApp.Model
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="Max 10 karakter belirtiniz.")]
        [DataType(DataType.Password)]
        [Display(Name ="Parola")]
        public string? Password { get; set; }

    }
}