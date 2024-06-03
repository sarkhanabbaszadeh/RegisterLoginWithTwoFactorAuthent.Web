using RegisterLoginWithTwoFactorAuthent.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace RegisterLoginWithTwoFactorAuthent.Web.ViewModels
{
	public class AuthenticatorViewModel
	{
        public string SharedKey { get; set; }

        public string AuthenticatorURL { get; set; }

        [Display(Name = "Doğrulama kodunuz")]
        [Required(ErrorMessage = "Doğrulama kodunuz lazımdır")]
        public string VerificationCode { get; set; }

		[Display(Name = "Iki faktorlu doğrulama tipiniz")]
		public TwoFactor TwoFactorType { get; set; }
    }
}
