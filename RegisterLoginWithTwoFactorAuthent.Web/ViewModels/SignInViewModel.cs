using System.ComponentModel.DataAnnotations;

namespace RegisterLoginWithTwoFactorAuthent.Web.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
            
        }

        public SignInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [EmailAddress(ErrorMessage = "E-poçta formatı səhvdir !")]
        [Required(ErrorMessage = "E-poçta boş buraxıla bilməz !")]
        [Display(Name = "E-poçta : ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifrə boş buraxıla bilməz !")]
        [Display(Name = "Şifrə : ")]
        public string Password { get; set; }

        [Display(Name = "Məni Xatırla")]
        public bool RememberMe { get; set; }
    }
}
