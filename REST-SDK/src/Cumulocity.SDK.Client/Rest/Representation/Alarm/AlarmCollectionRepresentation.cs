using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	[JsonObject]
	public class AlarmCollectionRepresentation : BaseCollectionRepresentation<AlarmRepresentation>
	{
		private IList<AlarmRepresentation> alarms;

		public virtual IList<AlarmRepresentation> Alarms
		{
			get
			{
				return alarms;
			}
			set
			{
				this.alarms = value;
			}
		}

		public override IEnumerator<AlarmRepresentation> GetEnumerator()
		{
			return alarms.GetEnumerator();
		}
	}
}