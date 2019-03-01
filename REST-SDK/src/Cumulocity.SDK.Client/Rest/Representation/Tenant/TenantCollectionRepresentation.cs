using Newtonsoft.Json;
using System.Collections.Generic;

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

		public override IEnumerator<TenantRepresentation> GetEnumerator()
		{
			return tenants.GetEnumerator();
		}
	}
}