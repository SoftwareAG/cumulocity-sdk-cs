using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Data @NoArgsConstructor @AllArgsConstructor public class DeliveryLogEntryRepresentation extends AbstractDynamicProperties
    public class DeliveryLogEntryRepresentation //: AbstractDynamicProperties
    {
        private string status;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Getter(onMethod = @_(@JSONConverter(type = DateTimeConverter.class))) private DateTime time;
        private DateTime time;
    }
}