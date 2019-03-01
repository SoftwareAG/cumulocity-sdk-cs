using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.HelperTest
{
	public class SecretPlatform
	{
		public string PlatformUrl { get; set; }
		public string UserAdmin { get; set; }
		public string PasswordAdmin { get; set; }

		public string UserBootstrap { get; set; }
		public string PasswordBootstrap { get; set; }
	}
}
