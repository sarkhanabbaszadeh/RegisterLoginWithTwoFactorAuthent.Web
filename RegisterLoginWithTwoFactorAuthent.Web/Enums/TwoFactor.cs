using System.ComponentModel.DataAnnotations;

namespace RegisterLoginWithTwoFactorAuthent.Web.Enums
{
	public enum TwoFactor
	{
		[Display(Name = "Hec biri")]
		None=0,
		[Display(Name = "Telefon ilə kimlik təsdiqləmə")]
		Phone = 1,
		[Display(Name = "E-poçta ilə kimlik təsdiqləmə")]
		Email = 2,
		[Display(Name = "Microsoft/Google ilə kimlik təsdiqləmə")]
		MicrosoftGoogle = 3,
	}
}
