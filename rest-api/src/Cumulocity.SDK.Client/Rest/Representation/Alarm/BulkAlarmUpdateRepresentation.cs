using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	public class BulkAlarmUpdateRepresentation : BaseResourceRepresentation
	{

		//ORIGINAL LINE: @Getter(onMethod = @__(@JSONProperty(ignoreIfNull = true))) @Setter private String status;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		private string status;

	}
}
