using Cumulocity.SDK.Client.Rest.Model.Idtype;

namespace Cumulocity.SDK.Client.Rest.Representation.Connector
{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Data @NoArgsConstructor @AllArgsConstructor public class ConnectorReferenceRepresentation extends AbstractDynamicProperties
    public class ConnectorReferenceRepresentation
    {
        private string domain;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Getter(onMethod = @_(@JSONConverter(type = IDTypeConverter.class))) private GId id;
        private GId id;
        private string name;
    }
}