using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap
{
	public class DeviceCredentialsRepresentation : CustomPropertiesMapRepresentation
	{
		private string id;

		private string tenantId;

		private string username;

		private string password;

		[JsonProperty("id")]
		public virtual string Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}

		[JsonProperty("tenantId")]
		public virtual string TenantId
		{
			get
			{
				return tenantId;
			}
			set
			{
				this.tenantId = value;
			}
		}

		[JsonProperty("username")]
		public virtual string Username
		{
			get
			{
				return username;
			}
			set
			{
				this.username = value;
			}
		}

		[JsonProperty("password")]
		public virtual string Password
		{
			get
			{
				return password;
			}
			set
			{
				this.password = value;
			}
		}
	}
}