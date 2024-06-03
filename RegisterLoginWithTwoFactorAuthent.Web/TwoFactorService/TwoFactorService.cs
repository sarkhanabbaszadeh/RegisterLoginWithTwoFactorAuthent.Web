using System.Text.Encodings.Web;

namespace RegisterLoginWithTwoFactorAuthent.Web.TwoFactorService
{
	public class TwoFactorService
	{
		private readonly UrlEncoder _urlEncoder;

		public TwoFactorService(UrlEncoder urlEncoder)
		{
			_urlEncoder = urlEncoder;
		}

		public string GenerateQrCodeUrl(string email, string unformattedKey)
		{
			const string format = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";

			return string.Format(format, _urlEncoder.Encode("www.2factor.az"), _urlEncoder.Encode(email), unformattedKey);
		}

	}
}
