using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
	public class ExternalIDRepresentation : BaseResourceRepresentation
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ExternalId { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Type { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ManagedObjectRepresentation ManagedObject { get; set; }
	}
}