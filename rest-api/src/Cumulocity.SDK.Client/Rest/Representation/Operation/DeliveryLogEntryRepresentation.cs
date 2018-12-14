using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{

    //ORIGINAL LINE: @Data @NoArgsConstructor @AllArgsConstructor public class DeliveryLogEntryRepresentation extends AbstractDynamicProperties
    public class DeliveryLogEntryRepresentation //: AbstractDynamicProperties
    {
        private string status;


        //ORIGINAL LINE: @Getter(onMethod = @_(@JSONConverter(type = DateTimeConverter.class))) private DateTime time;
        private DateTime time;
    }
}