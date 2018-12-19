using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.util;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	/// <summary>
	/// A filter to be used in measurement queries.
	/// The setter (by*) methods return the filter itself to provide chaining:
	/// {@code MeasurementFilter filter = new MeasurementFilter().byType(type).bySource(source);}
	/// </summary>
	public class MeasurementFilter : Filter
	{
		//ORIGINAL LINE: @ParamSource private String fragmentType;
		private string fragmentType;

		//ORIGINAL LINE: @ParamSource private String valueFragmentType;
		private string valueFragmentType;

		//ORIGINAL LINE: @ParamSource private String valueFragmentSeries;
		private string valueFragmentSeries;

		//ORIGINAL LINE: @ParamSource private String dateFrom;
		private string dateFrom;

		//ORIGINAL LINE: @ParamSource private String dateTo;
		private string dateTo;

		//ORIGINAL LINE: @ParamSource private String type;
		private string type;

		//ORIGINAL LINE: @ParamSource private String source;
		private string source;

		/// <summary>
		/// Specifies the {@code type} query parameter
		/// </summary>
		/// <param name="type"> the type of the event(s) </param>
		/// <returns> the event filter with {@code type} set </returns>
		public virtual MeasurementFilter byType(string type)
		{
			this.type = type;
			return this;
		}

		/// <summary>
		/// Specifies the {@code source} query parameter
		/// </summary>
		/// <param name="id"> the managed object that generated the event(s) </param>
		/// <returns> the event filter with {@code source} set </returns>
		public virtual MeasurementFilter bySource(GId id)
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
		public virtual MeasurementFilter bySource(ManagedObjectRepresentation source)
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

		public virtual MeasurementFilter byFragmentType(Type fragmentType)
		{
			this.fragmentType = ExtensibilityConverter.ClassToStringRepresentation(fragmentType);
			return this;
		}

		public virtual MeasurementFilter byFragmentType(string fragmentType)
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

		/// <summary>
		/// Specify value fragment type. This is preferred over the parameter {@code fragmentType}, because working with
		/// structured data and filtering via this parameter is lighter than filtering via {@code fragmentType}.
		/// </summary>
		/// <param name="valueFragmentType"> the value fragment type to filter. </param>
		/// <returns> the event filter with {@code valueFragmentType} set. </returns>
		/// <seealso cref= #byValueFragmentSeries(String) </seealso>
		/// <seealso cref= #byValueFragmentTypeAndSeries(String, String) </seealso>
		public virtual MeasurementFilter byValueFragmentType(string valueFragmentType)
		{
			this.valueFragmentType = valueFragmentType;
			return this;
		}

		/// <summary>
		/// Specify value fragment series, usually use in conjunction with <seealso cref="#byValueFragmentType(String)"/>
		/// </summary>
		/// <param name="valueFragmentSeries"> value fragment series to filter. </param>
		/// <returns> the event filter with {@code valueFragmentSeries} set. </returns>
		/// <seealso cref= #byValueFragmentType(String) </seealso>
		/// <seealso cref= #byValueFragmentTypeAndSeries(String, String) </seealso>
		public virtual MeasurementFilter byValueFragmentSeries(string valueFragmentSeries)
		{
			this.valueFragmentSeries = valueFragmentSeries;
			return this;
		}

		/// <summary>
		/// A short version combining of <seealso cref="#byValueFragmentType(String)"/> and <seealso cref="#byValueFragmentSeries(String)"/>.
		/// </summary>
		/// <param name="valueFragmentType"> value fragment type to filter, example: {@code c8y_TemperatureMeasurement} </param>
		/// <param name="valueFragmentSeries"> value fragment series to filter, example: {@code T} </param>
		/// <returns> the event filter with {@code valueFragmentType} and {@code valueFragmentSeries} set. </returns>
		public virtual MeasurementFilter byValueFragmentTypeAndSeries(string valueFragmentType, string valueFragmentSeries)
		{
			this.valueFragmentType = valueFragmentType;
			this.valueFragmentSeries = valueFragmentSeries;
			return this;
		}

		public virtual string ValueFragmentType
		{
			get
			{
				return valueFragmentType;
			}
		}

		public virtual string ValueFragmentSeries
		{
			get
			{
				return valueFragmentSeries;
			}
		}

		public virtual MeasurementFilter byDate(DateTime fromDate, DateTime toDate)
		{
			//DateConverter.date2String(fromDate);
			this.dateFrom = fromDate.ToString("o");
			this.dateTo = fromDate.ToString("o"); 
			return this;
		}

		public virtual MeasurementFilter byFromDate(DateTime fromDate)
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

	}
}
