using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;

namespace Cumulocity.SDK.Client.Rest.API.Alarm
{
	public interface IAlarmCollection :
		IPagedCollectionResource<AlarmRepresentation, PagedAlarmCollectionRepresentation<AlarmCollectionRepresentation>>
	{

	}
}
