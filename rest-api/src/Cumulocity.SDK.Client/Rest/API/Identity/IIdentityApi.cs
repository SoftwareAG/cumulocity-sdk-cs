using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;

namespace Cumulocity.SDK.Client.Rest.API.Identity
{
	public interface IIdentityApi
	{
		/// <summary>
		///     Creates an association between the external id and its global id in the platform.
		/// </summary>
		/// <param name="externalId"> the external to be created </param>
		/// <returns> the created external id </returns>
		/// <exception cref="SDKException"> if the external id could not be created </exception>
		ExternalIDRepresentation Create(ExternalIDRepresentation externalId);

		/// <summary>
		///     Gets external id representation from the platform by the given external id. The returned external id contains the
		///     managed object.
		///     The global id associated with the given external id can be extracted from that managed object
		/// </summary>
		/// <param name="extId"> id of the event to search for </param>
		/// <returns> the external id representation including the managed object </returns>
		/// <exception cref="SDKException"> if the external id is not found or if the query failed </exception>
		ExternalIDRepresentation GetExternalId(ID extId);

		/// <summary>
		///     Gets the external ids associated with the given global id
		/// </summary>
		/// <param name="gid"> the global of the external ids to search for </param>
		/// <returns> a collection of external ids with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IExternalIDCollection GetExternalIdsOfGlobalId(GId gid);

		/// <summary>
		///     Deletes between the external id and its global id in the platform.
		/// </summary>
		/// <param name="externalId"> the external id to be deleted </param>
		/// <exception cref="SDKException"> if the external could not be deleted </exception>
		void DeleteExternalId(ExternalIDRepresentation externalId);
	}
}