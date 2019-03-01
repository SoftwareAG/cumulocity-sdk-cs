using Cumulocity.SDK.Client.Rest.API.DeviceControl.Notification;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	public class DeviceControlApiImpl : IDeviceControlApi
	{
		private readonly RestConnector restConnector;

		private readonly int pageSize;

		private DeviceControlRepresentation deviceControlRepresentation;

		private readonly PlatformParameters parameters;

		private UrlProcessor urlProcessor;

		public DeviceControlApiImpl(PlatformParameters parameters, RestConnector restConnector, UrlProcessor urlProcessor, DeviceControlRepresentation deviceControlRepresentation, int pageSize)
		{
			this.parameters = parameters;
			this.restConnector = restConnector;
			this.urlProcessor = urlProcessor;
			this.deviceControlRepresentation = deviceControlRepresentation;
			this.pageSize = pageSize;
		}

		private DeviceControlRepresentation DeviceControlRepresentation
		{
			get
			{
				return deviceControlRepresentation;
			}
		}

		public OperationRepresentation GetOperation(GId gid)
		{
			string url = $"{SelfUri}/{gid.Value}";
			return restConnector.Get<OperationRepresentation>(url, DeviceControlMediaType.OPERATION, typeof(OperationRepresentation));
		}

		public IOperationCollection Operations
		{
			get
			{
				string url = SelfUri;
				return new OperationCollectionImpl(restConnector, url, pageSize);
			}
		}

		public OperationRepresentation Create(OperationRepresentation operation)
		{
			return restConnector.Post(SelfUri, DeviceControlMediaType.OPERATION, operation);
		}

		public OperationRepresentation Update(OperationRepresentation operation)
		{
			string url = $"{SelfUri}/{operation.Id.Value}";
			return restConnector.PutWithoutId(url, DeviceControlMediaType.OPERATION, prepareOperationForUpdate(operation));
		}

		public Task<OperationRepresentation> UpdateAsync(OperationRepresentation operation)
		{
			string url = $"{SelfUri}/{operation.Id.Value}";
			return restConnector.PutAsync(url, DeviceControlMediaType.OPERATION, prepareOperationForUpdate(operation));
		}

		private string SelfUri
		{
			get
			{
				return DeviceControlRepresentation.Operations.Self;
			}
		}

		private OperationRepresentation prepareOperationForUpdate(OperationRepresentation operation)
		{
			OperationRepresentation toSend = new OperationRepresentation();
			toSend.Status = operation.Status;
			if (OperationStatus.FAILED.ToString().Equals(operation.Status))
			{
				toSend.FailureReason = operation.FailureReason;
			}
			toSend.Attrs = operation.Attrs;
			return toSend;
		}

		public IOperationCollection GetOperationsByFilter(OperationFilter filter)
		{
			if (filter == null)
			{
				return Operations;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new OperationCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		public ISubscriber<GId, OperationRepresentation> NotificationsSubscriber
		{
			get
			{
				return new OperationNotificationSubscriber(parameters);
			}
		}
	}
}