using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
    public class ExternalIDRepresentation : BaseResourceRepresentation
    {

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Size(min = 1, message = "field cannot be empty")
		//private String externalId;
		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Size(min = 1, message = "field cannot be empty")
		//private String type;

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getExternalId()
	    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string ExternalId { get; set; }

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getType()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Type { get; set; }

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ManagedObjectRepresentation getManagedObject()
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual ManagedObjectRepresentation ManagedObject { get; set; }
    }
}