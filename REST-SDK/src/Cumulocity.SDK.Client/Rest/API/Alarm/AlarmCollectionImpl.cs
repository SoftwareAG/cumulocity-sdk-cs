using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;

namespace Cumulocity.SDK.Client.Rest.API.Alarm
{
	public class AlarmCollectionImpl : PagedCollectionResourceImpl<AlarmRepresentation, AlarmCollectionRepresentation, PagedAlarmCollectionRepresentation<AlarmCollectionRepresentation>>, IAlarmCollection
	{

		public AlarmCollectionImpl(RestConnector restConnector, string url, int pageSize) : base(restConnector, url, pageSize)
		{
		}

		protected internal override CumulocityMediaType MediaType
		{
			get
			{
				return AlarmMediaType.ALARM_COLLECTION;
			}
		}

		protected internal override Type ResponseClassProp => typeof(AlarmCollectionRepresentation);

		protected internal override PagedAlarmCollectionRepresentation<AlarmCollectionRepresentation> wrap(AlarmCollectionRepresentation collection)
		{
			return new PagedAlarmCollectionRepresentation<AlarmCollectionRepresentation>(collection, this);
		}
	}
}
