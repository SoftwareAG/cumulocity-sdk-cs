using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.util;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Event
{
	using System;

	/// <summary>
	/// A filter to be used in event queries.
	/// The setter (by*) methods return the filter itself to provide chaining:
	/// {@code EventFilter filter = new EventFilter().byType(type).bySource(source);}
	/// </summary>
	public class EventFilter : Filter
	{
		//ORIGINAL LINE: @ParamSource private String fragmentType;
		private string fragmentType;

		//ORIGINAL LINE: @ParamSource private String dateFrom;
		private string dateFrom;

		//ORIGINAL LINE: @ParamSource private String dateTo;
		private string dateTo;

		//ORIGINAL LINE: @ParamSource private String createdFrom;
		private string createdFrom;

		//ORIGINAL LINE: @ParamSource private String createdTo;
		private string createdTo;

		//ORIGINAL LINE: @ParamSource private String type;
		private string type;

		//ORIGINAL LINE: @ParamSource private String source;
		private string source;

		/// <summary>
		/// Specifies the {@code type} query parameter
		/// </summary>
		/// <param name="type"> the type of the event(s) </param>
		/// <returns> the event filter with {@code type} set </returns>
		public virtual EventFilter byType(string type)
		{
			this.type = type;
			return this;
		}

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="source"> the managed object that generated the event(s) </param>
		/// <returns> the event filter with {@code source} set </returns>
		public virtual EventFilter bySource(GId id)
		{
			this.source = id.Value;
			return this;
		}

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="source"> the managed object that generated the event(s) </param>
		/// <returns> the event filter with {@code source} set </returns>
		[Obsolete]
		public virtual EventFilter bySource(ManagedObjectRepresentation source)
		{
			this.source = source.Id.Value;
			return this;
		}

		/// <returns> the {@code type} parameter of the query </returns>
		public virtual string Type
		{
			get
			{
				return type;
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

		public virtual EventFilter byFragmentType(Type fragmentType)
		{
			this.fragmentType = ExtensibilityConverter.ClassToStringRepresentation(fragmentType);
			return this;
		}

		public virtual EventFilter byFragmentType(string fragmentType)
		{
			this.fragmentType = fragmentType;
			return this;
		}

		public virtual string FragmentType
		{
			get
			{
				return fragmentType;
			}
		}

		public virtual EventFilter byDate(DateTime fromDate, DateTime toDate)
		{
			//DateConverter.date2String
			this.dateFrom = fromDate.ToLongDateString();
			this.dateTo = toDate.ToLongDateString();
			return this;
		}

		public virtual EventFilter byFromDate(DateTime fromDate)
		{
			this.dateFrom = fromDate.ToLongDateString();
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

		public virtual string CreatedFrom
		{
			get
			{
				return createdFrom;
			}
		}

		public virtual string CreatedTo
		{
			get
			{
				return createdTo;
			}
		}

		public virtual EventFilter byCreationDate(DateTime fromDate, DateTime toDate)
		{
			//DateConverter.date2String
			this.createdFrom = fromDate.ToLongDateString();
			this.createdTo = toDate.ToLongDateString();
			return this;
		}

		public virtual EventFilter byFromCreationDate(DateTime fromDate)
		{
			//DateConverter.date2String
			this.createdFrom = fromDate.ToLongDateString();
			return this;
		}
	}

}
