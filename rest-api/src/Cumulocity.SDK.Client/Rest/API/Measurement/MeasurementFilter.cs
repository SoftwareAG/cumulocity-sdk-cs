using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.util;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	/// <summary>
	/// A filter to be used in measurement queries.
	/// The setter (by*) methods return the filter itself to provide chaining:
	/// {@code MeasurementFilter filter = new MeasurementFilter().ByType(type).BySource(source);}
	/// </summary>
	public class MeasurementFilter : Filter
	{
		private string fragmentType;

		private string valueFragmentType;

		private string valueFragmentSeries;

		private string dateFrom;

		private string dateTo;

		private string type;

		private string source;

		/// <summary>
		/// Specifies the {@code type} query parameter
		/// </summary>
		/// <param name="type"> the type of the event(s) </param>
		/// <returns> the event filter with {@code type} Set </returns>
		public virtual MeasurementFilter ByType(string type)
		{
			this.type = type;
			return this;
		}

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="id"> the managed object that generated the event(s) </param>
		/// <returns> the event filter with {@code source} Set </returns>
		public virtual MeasurementFilter BySource(GId id)
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
		public virtual MeasurementFilter BySource(ManagedObjectRepresentation source)
		{
			this.source = source.Id.Value;
			return this;
		}

		/// <returns> the {@code type} parameter of the query </returns>
		public virtual string Type => type;

		/// <returns> the {@code source} parameter of the query </returns>
		public virtual string Source
		{
			get
			{
				return source;
			}
		}

		public virtual MeasurementFilter byFragmentType(Type fragmentType)
		{
			this.fragmentType = ExtensibilityConverter.ClassToStringRepresentation(fragmentType);
			return this;
		}

		public virtual MeasurementFilter ByFragmentType(string fragmentType)
		{
			this.fragmentType = fragmentType;
			return this;
		}

		public virtual string FragmentType => fragmentType;

		/// <summary>
		/// Specify value fragment type. This is preferred over the parameter {@code fragmentType}, because working with
		/// structured data and filtering via this parameter is lighter than filtering via {@code fragmentType}.
		/// </summary>
		/// <param name="valueFragmentType"> the value fragment type to filter. </param>
		/// <returns> the event filter with {@code valueFragmentType} Set. </returns>
		/// <seealso cref= #ByValueFragmentSeries(String) </seealso>
		/// <seealso cref= #ByValueFragmentTypeAndSeries(String, String) </seealso>
		public virtual MeasurementFilter ByValueFragmentType(string valueFragmentType)
		{
			this.valueFragmentType = valueFragmentType;
			return this;
		}

		/// <summary>
		/// Specify value fragment series, usually use in conjunction with <seealso cref="#ByValueFragmentType(String)"/>
		/// </summary>
		/// <param name="valueFragmentSeries"> value fragment series to filter. </param>
		/// <returns> the event filter with {@code valueFragmentSeries} Set. </returns>
		/// <seealso cref= #ByValueFragmentType(String) </seealso>
		/// <seealso cref= #ByValueFragmentTypeAndSeries(String, String) </seealso>
		public virtual MeasurementFilter ByValueFragmentSeries(string valueFragmentSeries)
		{
			this.valueFragmentSeries = valueFragmentSeries;
			return this;
		}

		/// <summary>
		/// A short version combining of <seealso cref="#ByValueFragmentType(String)"/> and <seealso cref="#ByValueFragmentSeries(String)"/>.
		/// </summary>
		/// <param name="valueFragmentType"> value fragment type to filter, example: {@code c8y_TemperatureMeasurement} </param>
		/// <param name="valueFragmentSeries"> value fragment series to filter, example: {@code T} </param>
		/// <returns> the event filter with {@code valueFragmentType} and {@code valueFragmentSeries} Set. </returns>
		public virtual MeasurementFilter ByValueFragmentTypeAndSeries(string valueFragmentType, string valueFragmentSeries)
		{
			this.valueFragmentType = valueFragmentType;
			this.valueFragmentSeries = valueFragmentSeries;
			return this;
		}

		public virtual string ValueFragmentType => valueFragmentType;

		public virtual string ValueFragmentSeries => valueFragmentSeries;

		public virtual MeasurementFilter ByDate(DateTime fromDate, DateTime toDate)
		{
			this.dateFrom = fromDate.ToString("o");
			this.dateTo = toDate.ToString("o");
			return this;
		}

		public virtual MeasurementFilter ByFromDate(DateTime fromDate)
		{
			this.dateFrom = fromDate.ToString("o");
			return this;
		}

		public virtual string FromDate => dateFrom;

		public virtual string ToDate
		{
			get
			{
				return dateTo;
			}
		}
	}
}