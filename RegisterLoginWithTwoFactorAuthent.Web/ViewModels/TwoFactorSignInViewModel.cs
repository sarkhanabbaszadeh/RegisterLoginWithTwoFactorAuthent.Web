using RegisterLoginWithTwoFactorAuthent.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace RegisterLoginWithTwoFactorAuthent.Web.ViewModels
{
	public class TwoFactorSignInViewModel
	{
		[Display(Name ="Təsdiqləmə kodunuz")]
		[Required(ErrorMessage ="Təsdiqləmə kodu boş buraxıla bilməz !")]
		[StringLength(8,ErrorMessage ="Təsdiqləmə kodunuz ən çox 8 simvoldan ibarət olmalıdır.")]
        public string VerificationCode { get; set; }

        public bool isRememberMe { get; set; }

		public bool isRecoverCode { get; set; }

		public TwoFactor TwoFactorType { get; set; }
		}
}
