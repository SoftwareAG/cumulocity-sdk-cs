using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.util;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Event
{
	using System;

	/// <summary>
	/// A filter to be used in event queries.
	/// The setter (by*) methods return the filter itself to provide chaining:
	/// {@code EventFilter filter = new EventFilter().ByType(type).BySource(source);}
	/// </summary>
	public class EventFilter : Filter
	{
		private string fragmentType;

		private string dateFrom;

		private string dateTo;

		private string createdFrom;

		private string createdTo;

		private string type;

		private string source;

		/// <summary>
		/// Specifies the {@code type} query parameter
		/// </summary>
		/// <param name="type"> the type of the event(s) </param>
		/// <returns> the event filter with {@code type} Set </returns>
		public virtual EventFilter ByType(string type)
		{
			this.type = type;
			return this;
		}

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="source"> the managed object that generated the event(s) </param>
		/// <returns> the event filter with {@code source} Set </returns>
		public virtual EventFilter BySource(GId id)
		{
			this.source = id.Value;
			return this;
		}

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="source"> the managed object that generated the event(s) </param>
		/// <returns> the event filter with {@code source} Set </returns>
		[Obsolete]
		public virtual EventFilter BySource(ManagedObjectRepresentation source)
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
		public virtual string Source => source;

		public virtual EventFilter byFragmentType(Type fragmentType)
		{
			this.fragmentType = ExtensibilityConverter.ClassToStringRepresentation(fragmentType);
			return this;
		}

		public virtual EventFilter ByFragmentType(string fragmentType)
		{
			this.fragmentType = fragmentType;
			return this;
		}

		public virtual string FragmentType => fragmentType;

		public virtual EventFilter byDate(DateTime fromDate, DateTime toDate)
		{
			this.dateFrom = fromDate.ToString("o");
			this.dateTo = toDate.ToString("o");
			return this;
		}

		public virtual EventFilter byFromDate(DateTime fromDate)
		{
			this.dateFrom = fromDate.ToLongDateString();
			return this;
		}

		public virtual string FromDate => dateFrom;

		public virtual string ToDate => dateTo;

		public virtual string CreatedFrom => createdFrom;

		public virtual string CreatedTo => createdTo;

		public virtual EventFilter ByCreationDate(DateTime fromDate, DateTime toDate)
		{
			this.createdFrom = fromDate.ToLongDateString();
			this.createdTo = toDate.ToLongDateString();
			return this;
		}

		public virtual EventFilter ByFromCreationDate(DateTime fromDate)
		{
			this.createdFrom = fromDate.ToLongDateString();
			return this;
		}
	}
}