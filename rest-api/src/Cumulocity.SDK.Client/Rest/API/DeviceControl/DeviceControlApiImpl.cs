using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.API.DeviceControl.Notification;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;

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

		//ORIGINAL LINE: private DeviceControlRepresentation getDeviceControlRepresentation() throws SDKException
		private DeviceControlRepresentation DeviceControlRepresentation
		{
			get
			{
				return deviceControlRepresentation;
			}
		}

		//ORIGINAL LINE: @Override public OperationRepresentation getOperation(GId gid) throws SDKException
		public OperationRepresentation getOperation(GId gid)
		{
			string url = SelfUri + "/" + gid.Value;
			return restConnector.Get<OperationRepresentation>(url, DeviceControlMediaType.OPERATION, typeof(OperationRepresentation));
		}

		//ORIGINAL LINE: @Override public OperationCollection getOperations() throws SDKException
		public IOperationCollection Operations
		{
			get
			{
				string url = SelfUri;
				return new OperationCollectionImpl(restConnector, url, pageSize);
			}
		}

		//ORIGINAL LINE: @Override public OperationRepresentation create(OperationRepresentation operation) throws SDKException
		public OperationRepresentation create(OperationRepresentation operation)
		{
			return restConnector.Post(SelfUri, DeviceControlMediaType.OPERATION, operation);
		}

		//ORIGINAL LINE: @Override public OperationRepresentation update(OperationRepresentation operation) throws SDKException
		public OperationRepresentation update(OperationRepresentation operation)
		{
			string url = SelfUri + "/" + operation.Id.Value;
			return restConnector.PutWithoutId(url, DeviceControlMediaType.OPERATION, prepareOperationForUpdate(operation));
		}

		//ORIGINAL LINE: @Override public Future updateAsync(OperationRepresentation operation) throws SDKException
		//public override Future<> updateAsync(OperationRepresentation operation)
		//{
		//	string url = SelfUri + "/" + operation.Id.Value;
		//	return restConnector.putAsync(url, DeviceControlMediaType.OPERATION, prepareOperationForUpdate(operation));
		//}

		//ORIGINAL LINE: private String getSelfUri() throws SDKException
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

		//ORIGINAL LINE: @Override public OperationCollection getOperationsByFilter(OperationFilter filter) throws SDKException
		public IOperationCollection getOperationsByFilter(OperationFilter filter)
		{
			if (filter == null)
			{
				return Operations;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new OperationCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		//ORIGINAL LINE: @Override public Subscriber<GId, OperationRepresentation> getNotificationsSubscriber() throws SDKException
		public  ISubscriber<GId, OperationRepresentation> NotificationsSubscriber
		{
			get
			{
				return new OperationNotificationSubscriber(parameters);
			}
		}
	}
}