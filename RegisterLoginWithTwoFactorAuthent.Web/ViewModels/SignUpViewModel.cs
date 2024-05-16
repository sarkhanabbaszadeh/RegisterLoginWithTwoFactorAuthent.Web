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

        [Display(Name ="İstifadəçi adı : ")]
        public string UserName { get; set; }

        [Display(Name = "Şifrə : ")]
        public string Password { get; set; }

        [Display(Name = "Təkrar Şifrə : ")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Epoçta : ")]
        public string Email { get; set; }

        [Display(Name = "Telefon : ")]
        public string PhoneNumber { get; set; }
    }
}
