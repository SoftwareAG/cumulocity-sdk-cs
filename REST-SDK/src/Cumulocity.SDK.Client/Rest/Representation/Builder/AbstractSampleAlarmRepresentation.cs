using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
	public abstract class AbstractSampleAlarmRepresentation
	{
		public abstract AlarmRepresentationBuilder builder();

		public AlarmRepresentation build()
		{
			return builder().build();
		}
	}
}
