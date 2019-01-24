using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	public class BulkAlarmUpdateRepresentation : BaseResourceRepresentation
	{
#pragma warning disable 0169

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		private string status;
	}
}