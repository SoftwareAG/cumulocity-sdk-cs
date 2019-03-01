using Cumulocity.SDK.Client.Rest.Representation.Audit;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Newtonsoft.Json;
using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{


	public class AlarmRepresentation : EventRepresentation
	{

		private string status;

		private string severity;

		private AuditRecordCollectionRepresentation history;

		private long? count;

		private DateTime? firstOccurrenceTime;

		public AlarmRepresentation()
		{
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Status
		{
			get
			{
				return status;
			}
			set
			{
				this.status = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Severity
		{
			get
			{
				return severity;
			}
			set
			{
				this.severity = value;
			}
		}


		/// <summary>
		/// Kept for backwards compatibility in API. Should not be used. 
		/// @return
		/// </summary>
		[Obsolete]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual AuditRecordCollectionRepresentation History
		{
			get
			{
				return history;
			}
			set
			{
				this.history = value;
			}
		}
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual long? Count
		{
			get
			{
				return count;
			}
			set
			{
				this.count = value;
			}
		}

		[JsonProperty(propertyName: "firstOccurrenceTime", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime? FirstOccurrenceDateTime
		{
			get
			{
				return firstOccurrenceTime;
			}
			set
			{
				this.firstOccurrenceTime = value;
			}
		}

	}

}