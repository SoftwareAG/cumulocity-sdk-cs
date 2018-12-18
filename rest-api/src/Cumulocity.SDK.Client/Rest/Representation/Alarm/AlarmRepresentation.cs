using Cumulocity.SDK.Client.Rest.Representation.Audit;
using Cumulocity.SDK.Client.Rest.Representation.Event;
using Newtonsoft.Json;
using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{


	//ORIGINAL LINE: @EqualsAndHashCode public class AlarmRepresentation extends EventRepresentation
	public class AlarmRepresentation : EventRepresentation
	{

		private string status;

		//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private String severity;
		private string severity;

		private AuditRecordCollectionRepresentation history;

		//ORIGINAL LINE: @Null(operation = Command.UPDATE) private System.Nullable<long> count;
		private long? count;

		//ORIGINAL LINE: @Null(operation = Command.UPDATE) private DateTime firstOccurrenceTime;
		private DateTime? firstOccurrenceTime;

		public AlarmRepresentation()
		{
		}

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getStatus()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getSeverity()
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
		//ORIGINAL LINE: @Deprecated @JSONProperty(ignoreIfNull = true) public AuditRecordCollectionRepresentation getHistory()
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
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<long> getCount()
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

		//ORIGINAL LINE: @JSONProperty(value = "firstOccurrenceTime", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getFirstOccurrenceDateTime()
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


		//ORIGINAL LINE: @JSONProperty(value = "deprecated_FirstOccurrenceTime", ignore = true) @Deprecated public Date getFirstOccurrenceTime()
		//[Obsolete]
		//public virtual DateTime FirstOccurrenceTime
		//{
		//	get
		//	{
		//		return firstOccurrenceTime == null ? null : firstOccurrenceTime.toDate();
		//	}
		//	set
		//	{
		//		this.firstOccurrenceTime = value == null ? null : newLocal(value);
		//	}
		//}

	}

}