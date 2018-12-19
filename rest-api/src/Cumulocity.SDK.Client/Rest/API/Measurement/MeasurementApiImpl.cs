using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	public class MeasurementApiImpl: IMeasurementApi
	{

		private readonly RestConnector restConnector;

		private readonly int pageSize;

		private MeasurementsApiRepresentation measurementsApiRepresentation;

		private UrlProcessor urlProcessor;

		public MeasurementApiImpl(RestConnector restConnector, UrlProcessor urlProcessor, MeasurementsApiRepresentation measurementsApiRepresentation, int pageSize)
		{
			this.restConnector = restConnector;
			this.urlProcessor = urlProcessor;
			this.MeasurementApiRepresentation = measurementsApiRepresentation;
			this.pageSize = pageSize;
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: private MeasurementsApiRepresentation getMeasurementApiRepresentation() throws SDKException
		private MeasurementsApiRepresentation MeasurementApiRepresentation
		{
			get => measurementsApiRepresentation;
			set => measurementsApiRepresentation = value;
		}

		//ORIGINAL LINE: @Override public MeasurementRepresentation getMeasurement(GId measurementId) throws SDKException
		public  MeasurementRepresentation getMeasurement(GId measurementId)
		{
			string url = MeasurementApiRepresentation.measurements.Self + "/" + measurementId.Value;
			return restConnector.Get<MeasurementRepresentation>(url, MeasurementMediaType.MEASUREMENT, typeof(MeasurementRepresentation));
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: @Override @Deprecated public void deleteMeasurement(MeasurementRepresentation measurement) throws SDKException
		[Obsolete]
		public  void deleteMeasurement(MeasurementRepresentation measurement)
		{
			delete(measurement);
		}

		//ORIGINAL LINE: @Override public void delete(MeasurementRepresentation measurement) throws SDKException
		public  void delete(MeasurementRepresentation measurement)
		{
			string url = MeasurementApiRepresentation.measurements.Self + "/" + measurement.Id.Value;
			restConnector.Delete(url);
		}

		//ORIGINAL LINE: @Override public void deleteMeasurementsByFilter(MeasurementFilter filter) throws IllegalArgumentException, SDKException
		public  void deleteMeasurementsByFilter(MeasurementFilter filter)
		{
			if (filter == null)
			{
				throw new System.ArgumentException("Measurement filter is null");
			}
			else
			{
				IDictionary<string, string> @params = filter.QueryParams;
				restConnector.Delete(urlProcessor.replaceOrAddQueryParam(SelfUri, @params));
			}
		}
		//ORIGINAL LINE: @Override public MeasurementCollection getMeasurementsByFilter(MeasurementFilter filter) throws SDKException
		public  IMeasurementCollection getMeasurementsByFilter(MeasurementFilter filter)
		{
			if (filter == null)
			{
				return Measurements;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new MeasurementCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		//ORIGINAL LINE: @Override public MeasurementCollection getMeasurements() throws SDKException
		public  IMeasurementCollection Measurements
		{
			get
			{
				string url = MeasurementApiRepresentation.measurements.Self;
				return new MeasurementCollectionImpl(restConnector, url, pageSize);
			}
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: @Override public MeasurementRepresentation create(MeasurementRepresentation measurementRepresentation) throws SDKException
		public  MeasurementRepresentation create(MeasurementRepresentation measurementRepresentation)
		{
			return restConnector.Post(SelfUri, MeasurementMediaType.MEASUREMENT, measurementRepresentation);
		}

		public  MeasurementCollectionRepresentation createBulk(MeasurementCollectionRepresentation measurementCollection)
		{
			return restConnector.Post(SelfUri, MeasurementMediaType.MEASUREMENT_COLLECTION, measurementCollection);
		}

		public  void createBulkWithoutResponse(MeasurementCollectionRepresentation measurementCollection)
		{
			restConnector.PostWithoutResponse(SelfUri, MeasurementMediaType.MEASUREMENT_COLLECTION, measurementCollection);
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: @Override public void createWithoutResponse(MeasurementRepresentation measurementRepresentation) throws SDKException
		public  void createWithoutResponse(MeasurementRepresentation measurementRepresentation)
		{
			restConnector.PostWithoutResponse(SelfUri, MeasurementMediaType.MEASUREMENT, measurementRepresentation);
		}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: @Override public Future createAsync(MeasurementRepresentation measurementRepresentation) throws SDKException
		//public override Future createAsync(MeasurementRepresentation measurementRepresentation)
		//{
		//	return restConnector.postAsync(SelfUri, MeasurementMediaType.MEASUREMENT, measurementRepresentation);
		//}

		//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		//ORIGINAL LINE: protected String getSelfUri() throws SDKException
		protected internal virtual string SelfUri
		{
			get
			{
				return MeasurementApiRepresentation.measurements.Self;
			}
		}


	}
}
