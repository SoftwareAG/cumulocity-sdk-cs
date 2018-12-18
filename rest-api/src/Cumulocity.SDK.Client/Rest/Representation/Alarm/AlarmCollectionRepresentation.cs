using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	[JsonObject]
	public class AlarmCollectionRepresentation : BaseCollectionRepresentation<AlarmRepresentation>
	{

		private IList<AlarmRepresentation> alarms;

		//ORIGINAL LINE: @JSONTypeHint(AlarmRepresentation.class) public List<AlarmRepresentation> getAlarms()
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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<AlarmRepresentation> iterator()
		public IEnumerator<AlarmRepresentation> iterator()
		{
			return alarms.GetEnumerator();
		}

		public override IEnumerator<AlarmRepresentation> GetEnumerator()
		{
			return alarms.GetEnumerator();
		}
	}
}
