using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap
{
	public class DeviceCredentialsRepresentation : CustomPropertiesMapRepresentation
	{
		//ORIGINAL LINE: @NotNull(operation = {CREATE}) private String id;
		private string id;

		//ORIGINAL LINE: @Null(operation = {CREATE}) private String tenantId;
		private string tenantId;

		//ORIGINAL LINE: @Null(operation = {CREATE}) private String username;
		private string username;

		//ORIGINAL LINE: @Null(operation = {CREATE}) private String password;
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