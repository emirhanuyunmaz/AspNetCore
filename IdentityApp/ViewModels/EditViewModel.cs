using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels
{
    public class EditViewModel
    {
        public string? Id { get; set; }
        public string? UserName { get; set; } 
        
        public string? FullName { get; set; } 
        
        
        public string? Email { get; set; } 
        
        
        [DataType(DataType.Password)]
        public string? Password { get; set; } 
        
        
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Parola Eşleşmiyor")]
        public string? ConfirmPassword { get; set; } 

        public IList<string>? SelectedRole { get; set; }

    }
}