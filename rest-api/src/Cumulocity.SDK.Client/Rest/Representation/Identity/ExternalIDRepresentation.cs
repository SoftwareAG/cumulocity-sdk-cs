using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
    public class ExternalIDRepresentation : BaseResourceRepresentation
    {
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Size(min = 1, message = "field cannot be empty") private String externalId;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NotNull(operation = Command.CREATE) @Size(min = 1, message = "field cannot be empty") private String type;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getExternalId()
        public virtual string ExternalId { get; set; }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getType()
        public virtual string Type { get; set; }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public ManagedObjectRepresentation getManagedObject()
        public virtual ManagedObjectRepresentation ManagedObject { get; set; }
    }
}