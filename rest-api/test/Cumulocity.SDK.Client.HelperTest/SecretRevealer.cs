using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.HelperTest;
using Microsoft.Extensions.Options;

namespace Cumulocity.SDK.Client.HelperTest
{
	public class SecretRevealer : ISecretRevealer
	{
		private readonly SecretPlatform _secrets;
		public SecretRevealer(IOptions<SecretPlatform> secrets)
		{
			_secrets = secrets.Value ?? throw new ArgumentNullException(nameof(secrets));
		}

		public (string user, string pass) Reveal()
		{

			return (_secrets.UserAdmin, _secrets.PasswordAdmin);
		}
	}
}
