﻿using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Measurement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Measurement
{
	public class MeasurementApiImpl : IMeasurementApi
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

		private MeasurementsApiRepresentation MeasurementApiRepresentation
		{
			get => measurementsApiRepresentation;
			set => measurementsApiRepresentation = value;
		}

		public MeasurementRepresentation getMeasurement(GId measurementId)
		{
			string url = MeasurementApiRepresentation.measurements.Self + "/" + measurementId.Value;
			return restConnector.Get<MeasurementRepresentation>(url, MeasurementMediaType.MEASUREMENT, typeof(MeasurementRepresentation));
		}

		[Obsolete]
		public void deleteMeasurement(MeasurementRepresentation measurement)
		{
			delete(measurement);
		}

		public void delete(MeasurementRepresentation measurement)
		{
			string url = MeasurementApiRepresentation.measurements.Self + "/" + measurement.Id.Value;
			restConnector.Delete(url);
		}

		public void deleteMeasurementsByFilter(MeasurementFilter filter)
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

		public IMeasurementCollection getMeasurementsByFilter(MeasurementFilter filter)
		{
			if (filter == null)
			{
				return Measurements;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new MeasurementCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		public IMeasurementCollection Measurements
		{
			get
			{
				string url = MeasurementApiRepresentation.measurements.Self;
				return new MeasurementCollectionImpl(restConnector, url, pageSize);
			}
		}

		public MeasurementRepresentation create(MeasurementRepresentation measurementRepresentation)
		{
			return restConnector.Post(SelfUri, MeasurementMediaType.MEASUREMENT, measurementRepresentation);
		}

		public MeasurementCollectionRepresentation createBulk(MeasurementCollectionRepresentation measurementCollection)
		{
			return restConnector.Post(SelfUri, MeasurementMediaType.MEASUREMENT_COLLECTION, measurementCollection);
		}

		public void createBulkWithoutResponse(MeasurementCollectionRepresentation measurementCollection)
		{
			restConnector.PostWithoutResponse(SelfUri, MeasurementMediaType.MEASUREMENT_COLLECTION, measurementCollection);
		}

		public void createWithoutResponse(MeasurementRepresentation measurementRepresentation)
		{
			restConnector.PostWithoutResponse(SelfUri, MeasurementMediaType.MEASUREMENT, measurementRepresentation);
		}

		public Task<MeasurementRepresentation> createAsync(MeasurementRepresentation measurementRepresentation)
		{
			return restConnector.PostAsync(SelfUri, MeasurementMediaType.MEASUREMENT, measurementRepresentation);
		}

		protected internal virtual string SelfUri
		{
			get
			{
				return MeasurementApiRepresentation.measurements.Self;
			}
		}
	}
}