namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	public class TenantReferenceRepresentation : BaseResourceRepresentation
	{
		private TenantRepresentation tenant;

		public virtual TenantRepresentation Tenant
		{
			get
			{
				return tenant;
			}
			set
			{
				this.tenant = value;
			}
		}
	}
}