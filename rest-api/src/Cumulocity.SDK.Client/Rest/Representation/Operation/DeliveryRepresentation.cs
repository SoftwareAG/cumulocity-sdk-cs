using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Operation
{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Data public class DeliveryRepresentation extends AbstractDynamicProperties
    public class DeliveryRepresentation //: AbstractDynamicProperties
    {
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Getter(onMethod = @_(@JSONTypeHint(DeliveryLogEntryRepresentation.class))) private List<DeliveryLogEntryRepresentation> log;
        private IList<DeliveryLogEntryRepresentation> log;

        private string status;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Getter(onMethod = @_(@JSONConverter(type = DateTimeConverter.class))) private DateTime time;
        private DateTime time;
    }
}