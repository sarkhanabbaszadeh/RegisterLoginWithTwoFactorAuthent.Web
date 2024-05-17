using System.ComponentModel.DataAnnotations;

namespace RegisterLoginWithTwoFactorAuthent.Web.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        {
                
        }

        public SignUpViewModel(string userName, string password, string email, string phoneNumber)
        {
            UserName = userName;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        [Required(ErrorMessage="İstifadəçi adı boş buraxıla bilməz !")]
        [Display(Name ="İstifadəçi adı : ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifrə boş buraxıla bilməz !")]
        [Display(Name = "Şifrə : ")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifrə digəri ilə uyğun deyil !")]
        [Required(ErrorMessage = "Təkrar Şifrə boş buraxıla bilməz !")]
        [Display(Name = "Təkrar Şifrə : ")]
        public string PasswordConfirm { get; set; }

        [EmailAddress(ErrorMessage = "Epoçta formatı yalnışdır !")]
        [Required(ErrorMessage = "Epoçta boş buraxıla bilməz !")]
        [Display(Name = "Epoçta : ")]
        public string Email { get; set; }

        [Display(Name = "Telefon : ")]
        public string PhoneNumber { get; set; }
    }
}
