using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace ExampleApp.Utils
{
	public class SecretRevealer : ISecretRevealer
	{
		private readonly SecretStuff _secrets;
		public SecretRevealer(IOptions<SecretStuff> secrets)
		{
			_secrets = secrets.Value ?? throw new ArgumentNullException(nameof(secrets));
		}

		public (string user, string pass) Reveal()
		{

			return (_secrets.UserAdmin, _secrets.PasswordAdmin);
		}
	}
}
