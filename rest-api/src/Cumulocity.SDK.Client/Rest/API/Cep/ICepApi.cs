using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cumulocity.SDK.Client.Rest.API.Cep.Notification;
using Cumulocity.SDK.Client.Rest.Representation.Cep;

namespace Cumulocity.SDK.Client.Rest.API.Cep
{
	/// <summary>
	/// API for integration with Custom Event Processing modules from the platform.
	/// 
	/// </summary>
	public interface ICepApi
	{

		/// <summary>
		/// Gets the notifications subscriber, which allows to receive notifications sent from cep.
		/// <pre>
		/// <code>
		/// Example:
		/// 
		///  Subscriber<String, Object> subscriber = deviceControlApi.getNotificationsSubscriber();
		/// 
		///  subscriber.subscirbe( "channelId" , new SubscriptionListener<String, Object>() {
		/// 
		///      {@literal @}Override
		///      public void onNotification(Subscription<GId> subscription, Object operation) {
		///             //Process notification from cep module 
		///      }
		/// 
		///      {@literal @}Override
		///      public void onError(Subscription<GId> subscription, Throwable ex) {
		///          // handle Subscribe error
		///      }
		///  });
		///  </code>
		///  </pre>
		/// </summary>
		/// <returns> subscriber </returns>
		/// <exception cref="SDKException"> </exception>
		CepCustomNotificationsSubscriber CustomNotificationsSubscriber { get; }

		/// <summary>
		/// Gets an cep module by id
		/// </summary>
		/// <param name="id"> of the cep module to search for </param>
		/// <returns> the cep module with the given id </returns>
		/// <exception cref="SDKException"> if the cep module is not found or if the query failed </exception>
		CepModuleRepresentation Get(string id);

		/// <summary>
		/// Gets a cep module text by id
		/// </summary>
		/// <param name="id"> of the cep module to search for </param>
		/// <returns> the cep module text </returns>
		/// <exception cref="SDKException"> if the cep module is not found or if the query failed </exception>
		string GetText(string id);

		/// <summary>
		/// Creates an cep module in the platform.
		/// </summary>
		/// <param name="content"> input stream to resource with cep module definition </param>
		/// <returns> the created cep module with the generated id </returns>
		/// <exception cref="SDKException"> if the cep module could not be created </exception>
		[Obsolete]
		CepModuleRepresentation Create(Stream content);

		/// <summary>
		/// Creates an cep module in the platform.
		/// </summary>
		/// <param name="content"> of cep module definition </param>
		/// <returns> the created cep module with the generated id </returns>
		/// <exception cref="SDKException"> if the cep module could not be created </exception>
		CepModuleRepresentation Create(string content);
		/// <summary>
		/// Updates an cep module in the platform.
		/// The cep module to be updated is identified by the id.
		/// </summary>
		/// <param name="id"> of cep module to Update </param>
		/// <param name="content"> input stream to resource with cep module definition </param>
		/// <returns> the updated cep module </returns>
		/// <exception cref="SDKException"> if the cep module could not be updated </exception>
		CepModuleRepresentation Update(string id, Stream content);

		CepModuleRepresentation Update(string id, string content);

		CepModuleRepresentation Update(CepModuleRepresentation module);

		/// <summary>
		/// Gets all cep modules from the platform
		/// </summary>
		/// <returns> collection of cep modules with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		ICepModuleCollection Modules { get; }

		/// <summary>
		/// Deletes the cep module from the platform.
		/// </summary>
		/// <exception cref="SDKException"> </exception>
		void Delete(CepModuleRepresentation module);

		/// <summary>
		/// Deletes the cep module from the platform.
		/// </summary>
		/// <exception cref="SDKException"> </exception>
		void Delete(string id);

		/// <summary>
		/// Checks state of cep microservice.
		/// </summary>
		T Health<T>(Type clazz);
	}
}
