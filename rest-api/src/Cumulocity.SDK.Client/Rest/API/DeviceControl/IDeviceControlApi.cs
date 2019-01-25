﻿using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	/// <summary>
	/// API for creating, updating and retrieving operations from the platform.
	/// </summary>
	public interface IDeviceControlApi
	{
		/// <summary>
		/// Gets operation by id
		/// </summary>
		/// <param name="gid"> id of the operation to search for </param>
		/// <returns> the operation with the given id </returns>
		/// <exception cref="SDKException"> if the operation is not found or if the query failed </exception>
		OperationRepresentation GetOperation(GId gid);

		/// <summary>
		/// Creates operation in the platform. The id of the operation must not be set, since it will be generated by the platform
		/// </summary>
		/// <param name="operation"> operation to be created </param>
		/// <returns> the created operation with the generated id </returns>
		/// <exception cref="SDKException"> if the operation could not be created </exception>
		OperationRepresentation Create(OperationRepresentation operation);

		/// <summary>
		/// Updates operation in the platform.
		/// The operation to be updated is identified by the id within the given operation.
		/// </summary>
		/// <param name="operation"> to be updated </param>
		/// <returns> the updated operation </returns>
		/// <exception cref="SDKException"> if the operation could not be updated </exception>
		OperationRepresentation Update(OperationRepresentation operation);

		/// <summary>
		/// Updates operation in the platform. Immediate response is available through the Future object.
		/// In case of lost connection, buffers data in persistence provider.
		/// </summary>
		/// <param name="operation"> to be updated </param>
		/// <returns> the updated operation </returns>
		/// <exception cref="SDKException"> if the operation could not be updated </exception>
		Task<OperationRepresentation> UpdateAsync(OperationRepresentation operation);

		/// <summary>
		/// Gets the all the operation in the platform
		/// </summary>
		/// <returns> collection of operations with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>

		IOperationCollection Operations { get; }

		/// <summary>
		/// Gets the operations from the platform based on specified filter. Queris based on [{@code status}, {@code deviceId}, {@code agentId}]
		/// and [{@code deviceId}, {@code agentId}] are not supported.
		/// currently not supported.
		/// </summary>
		/// <param name="filter"> the filter criteria(s) </param>
		/// <returns> collection of operations matched by the filter with paging functionality </returns>
		/// <exception cref="SDKException">             if the query failed </exception>
		/// <exception cref="IllegalArgumentException"> in case of queries based on [{@code status}, {@code deviceId}, {@code agentId}] or [{@code
		///                                  deviceId}, {@code agentId}] </exception>
		IOperationCollection GetOperationsByFilter(OperationFilter filter);

		/// <summary>
		/// Gets the notifications subscriber, which allows to receive newly created operations for agent.
		/// <pre>
		/// <code>
		/// Example:
		///
		///  final GId agentId = ...
		///  Subscriber<GId, OperationRepresentation> subscriber = deviceControlApi.getNotificationsSubscriber();
		///
		///  subscriber.subscirbe( agentId , new SubscriptionListener<GId, OperationRepresentation>() {
		///
		///      {@literal @}Override
		///      public void OnNotification(Subscription<GId> subscription, OperationRepresentation operation) {
		///             //Process operation
		///      }
		///
		///      {@literal @}Override
		///      public void OnError(Subscription<GId> subscription, Throwable ex) {
		///          // handle Subscribe operation error
		///      }
		///  });
		///  </code>
		///  </pre>
		/// </summary>
		/// <returns> subscriber </returns>
		/// <exception cref="SDKException"> </exception>
		ISubscriber<GId, OperationRepresentation> NotificationsSubscriber { get; }
	}
}