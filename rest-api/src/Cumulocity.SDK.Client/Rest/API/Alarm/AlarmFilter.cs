using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Event;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Alarm
{
	public class AlarmFilter : Filter
	{

		//ORIGINAL LINE: @ParamSource private String status;
		private string status;

		//ORIGINAL LINE: @ParamSource private String source;
		private string source;

		//ORIGINAL LINE: @ParamSource private String dateFrom;
		private string dateFrom;

		//ORIGINAL LINE: @ParamSource private String dateTo;
		private string dateTo;

		//ORIGINAL LINE: @ParamSource private String severity;
		private string severity;

		//ORIGINAL LINE: @ParamSource private String resolved;
		private string resolved;

		//ORIGINAL LINE: @ParamSource private String type;
		private string type;

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="source"> the managed object that generated the alarm(s) </param>
		/// <returns> the alarm filter with {@code source} set </returns>
		public virtual AlarmFilter bySource(GId source)
		{
			this.source = source.Value;
			return this;
		}

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="source"> the managed object that generated the alarm(s) </param>
		/// <returns> the alarm filter with {@code source} set </returns>
		[Obsolete]
		public virtual AlarmFilter bySource(ManagedObjectRepresentation source)
		{
			this.source = source.Id.Value;
			return this;
		}

		/// <summary>
		/// Specifies the {@code status} query parameter
		/// </summary>
		/// <param name="status"> status of the alarm(s) </param>
		/// <returns> the alarm filter with {@code status} set </returns>
		public virtual AlarmFilter byStatus(params CumulocityAlarmStatuses[] statuses)
		{
			if (statuses == null)
			{
				this.status = null;
				return this;
			}
			StringBuilder tmp = new StringBuilder();
			for (int index = 0; index <= statuses.Length - 1; index++)
			{
				tmp.Append(statuses[index].ToString());
				if (index < statuses.Length - 1)
				{
					tmp.Append(",");
				}
			}
			this.status = tmp.ToString();
			return this;

		}

		/// <returns> the {@code status} parameter of the query </returns>
		public virtual string Status
		{
			get
			{
				return status;
			}
		}

		/// <summary>
		/// Specifies the {@code status} query parameter
		/// </summary>
		/// <param name="status"> status of the alarm(s) </param>
		/// <returns> the alarm filter with {@code status} set </returns>
		public virtual AlarmFilter bySeverity(CumulocitySeverities severity)
		{
			this.severity = severity.ToString();
			return this;
		}

		/// <returns> the {@code status} parameter of the query </returns>
		public virtual string Severity
		{
			get
			{
				return severity;
			}
		}


		/// <returns> the {@code source} parameter of the query </returns>
		public virtual string Source
		{
			get
			{
				return source;
			}
		}


		/// <summary>
		/// Specifies the {@code fromDate} and {@code toDate} query parameters 
		/// for query in a time range.
		/// </summary>
		/// <param name="fromDate"> the start date time of the range </param>
		/// <param name="toDate"> the end date time of the range </param>
		/// <returns> the alarm filter with {@code fromDate} and {@code toDate} set. </returns>
		public virtual AlarmFilter byDate(DateTime fromDate, DateTime toDate)
		{
			//DateConverter.date2String
			this.dateFrom = fromDate.ToString("o");
			this.dateTo = toDate.ToString("o");
			return this;
		}

		/// <summary>
		/// Specifies the {@code fromDate} query parameter 
		/// for querying all alarms from the specified date time.
		/// </summary>
		/// <param name="fromDate"> the date time from which all alarms to be returned. </param>
		/// <returns> the alarm filter with {@code fromDate} set </returns>
		public virtual AlarmFilter byFromDate(DateTime fromDate)
		{
			this.dateFrom = fromDate.ToString("o");
			return this;
		}

		public virtual string FromDate
		{
			get
			{
				return dateFrom;
			}
		}

		public virtual string ToDate
		{
			get
			{
				return dateTo;
			}
		}

		public virtual string Resolved
		{
			get
			{
				return resolved;
			}
		}

		public virtual AlarmFilter byResolved(bool? resolved)
		{
			this.resolved = resolved.ToString();
			return this;
		}

		public virtual string Type
		{
			get
			{
				return type;
			}
		}

		public virtual AlarmFilter byType(string type)
		{
			this.type = type;
			return this;
		}


	}

}
