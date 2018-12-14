using Cumulocity.SDK.Client.Rest.Representation.Event;

namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	using System;

	public class AlarmRepresentation : EventRepresentation
	{

		//private string status;


		////ORIGINAL LINE: @NotNull(operation = Command.CREATE) private String severity;
		//private string severity;

		//private AuditRecordCollectionRepresentation history;

		////ORIGINAL LINE: @Null(operation = Command.UPDATE) private System.Nullable<long> count;
		//private long? count;

		////ORIGINAL LINE: @Null(operation = Command.UPDATE) private DateTime firstOccurrenceTime;
		//private DateTime firstOccurrenceTime;

		//public AlarmRepresentation()
		//{
		//}

		////ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getStatus()
		//public virtual string Status
		//{
		//	get
		//	{
		//		return status;
		//	}
		//	set
		//	{
		//		this.status = value;
		//	}
		//}

		////ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getSeverity()
		//public virtual string Severity
		//{
		//	get
		//	{
		//		return severity;
		//	}
		//	set
		//	{
		//		this.severity = value;
		//	}
		//}


		///// <summary>
		///// Kept for backwards compatibility in API. Should not be used. 
		///// @return
		///// </summary>
		////ORIGINAL LINE: @Deprecated @JSONProperty(ignoreIfNull = true) public AuditRecordCollectionRepresentation getHistory()
		//[Obsolete]
		//public virtual AuditRecordCollectionRepresentation History
		//{
		//	get
		//	{
		//		return history;
		//	}
		//	set
		//	{
		//		this.history = value;
		//	}
		//}

		////ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<long> getCount()
		//public virtual long? Count
		//{
		//	get
		//	{
		//		return count;
		//	}
		//	set
		//	{
		//		this.count = value;
		//	}
		//}

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


		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(value = "firstOccurrenceTime", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getFirstOccurrenceDateTime()
		//public virtual DateTime FirstOccurrenceDateTime
		//{
		//	get
		//	{
		//		return firstOccurrenceTime;
		//	}
		//	set
		//	{
		//		this.firstOccurrenceTime = value;
		//	}
		//}

	}

}