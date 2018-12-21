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