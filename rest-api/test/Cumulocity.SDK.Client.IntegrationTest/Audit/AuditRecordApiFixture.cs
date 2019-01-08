using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.IntegrationTest.Common;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Cumulocity.SDK.Client.IntegrationTest.Audit
{
	public class AuditRecordApiFixture:IDisposable
	{
		private TenantCreator tenantCreator;
		public PlatformImpl platform;


		public AuditRecordApiFixture()
		{
			//this.platform = createPlatform();
			//this.tenantCreator = new TenantCreator(platform);
			//tenantCreator.createTenantAsync().Wait(500);
			//System.Threading.Thread.Sleep(5000);
		}

		private static PlatformImpl createPlatform()
		{
			var p = readSystemPropertie();

			return new PlatformImpl(
				p["cumulocity.host"],
				new CumulocityCredentials(p["cumulocity.tenant"],
					p["cumulocity.user"],
					p["cumulocity.password"],
					null),
				5);
		}

		private static Dictionary<string, string> readSystemPropertie()
		{
			var result = new Dictionary<string, string>();

			var location = typeof(AuditRecordApiIT).GetTypeInfo().Assembly.Location;
			var dirPath = Path.GetDirectoryName(location);
			var appConfigPath = Path.Combine(dirPath, "Resources/cumulocity-test.properties");
			var readText = File.ReadAllLines(appConfigPath, Encoding.UTF8);
			foreach (var item in readText)
			{
				var arr = item.Split('=');
				if (arr != null && arr.Length == 2)
				{
					if (arr[0].StartsWith("#") || result.ContainsKey(arr[0]))
						continue;
					result.Add(arr[0], arr[1]);
				}
			}
			return result;
		}

		public void Dispose()
		{
			tenantCreator.removeTenant();
		}
	}
}