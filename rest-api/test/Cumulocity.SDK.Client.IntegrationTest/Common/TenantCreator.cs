using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;
using Cumulocity.SDK.Client.Rest.Utils;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Common
{
	internal class TenantCreator
	{
		private const string TENANT_URI = "tenant/tenants";

		private readonly PlatformImpl platform;

		public TenantCreator(PlatformImpl platform)
		{
			this.platform = platform;
		}

		public virtual async Task createTenantAsync()
		{
			var tr = await postNewTenant();
			Assert.Equal(201, (int)tr.StatusCode);
		}

		public void removeTenant()
		{
			var tenantResponse =  deleteTenant();
			Assert.Equal(204, (int)tenantResponse.StatusCode);
		}

		private async Task<HttpResponseMessage> postNewTenant()
		{
			string host = platform.Host;
			string tenantId = platform.TenantId;
			string tenantJson =
				$"{{ \"id\": \"{tenantId}\", \"domain\": \"{tenantId}.staging.c8y.io\", \"company\": \"sample-tenant\", \"adminName\": \"{platform.User}\", \"adminPass\": \"{platform.Password}\" }}";

			using (var client = new HttpClient())
			using (var request = new HttpRequestMessage(HttpMethod.Post, host + TENANT_URI))
			{
				using (var stringContent = new StringContent(tenantJson, Encoding.UTF8).Replace(TenantMediaType.TENANT_TYPE))
				{
					client.DefaultRequestHeaders.Authorization
						= new AuthenticationHeaderValue("Basic",
							Convert.ToBase64String(
								System.Text.Encoding.ASCII.GetBytes(
									$"{"piotr/admin"}:{"test1234"}")));

					request.Content = stringContent;

					return await client
						.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
						.ConfigureAwait(false);
				}
			}
		}

		public virtual HttpResponseMessage deleteTenant()
		{
			string host = "https://managment.staging.c8y.io/";

			using (var client = new HttpClient())
			using (var request = new HttpRequestMessage(HttpMethod.Delete, host + TENANT_URI + "/" + platform.TenantId))
			{
				client.DefaultRequestHeaders.Authorization
					= new AuthenticationHeaderValue("Basic",
						Convert.ToBase64String(
							System.Text.Encoding.ASCII.GetBytes(
								$"{"management/admin"}:{"Pyi1co1s"}")));

				return client.SendAsync(request).Result;
			}
		}
	}
}
