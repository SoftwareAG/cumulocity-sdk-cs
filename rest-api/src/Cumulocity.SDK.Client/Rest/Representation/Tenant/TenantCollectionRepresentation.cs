using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	[JsonObject]
	public class TenantCollectionRepresentation : BaseCollectionRepresentation<TenantRepresentation>
	{
		private IList<TenantRepresentation> tenants;

		public virtual IList<TenantRepresentation> Tenants
		{
			get
			{
				return tenants;
			}
			set
			{
				this.tenants = value;
			}
		}

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<TenantRepresentation> iterator()
		public IEnumerator<TenantRepresentation> iterator()
		{
			return tenants.GetEnumerator();
		}

		public override IEnumerator<TenantRepresentation> GetEnumerator()
		{
			return tenants.GetEnumerator();
		}
	}
}