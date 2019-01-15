using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public interface IInventoryApi//<T> where T : ManagedObjectCollectionRepresentation
	{
		/// <summary>
		/// Returns the Managed Object of the Resource.
		/// </summary>
		/// <returns> ManagedObjectRepresentation </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation Get(GId id);

		/// <summary>
		/// Deletes the Managed Object from the Cumulocity Server.
		/// </summary>
		/// <exception cref="SDKException"> </exception>
		void delete(GId id);

		/// <summary>
		/// This update the ManagedObject for the operationCollection. Cannot update the ID.
		/// </summary>
		/// <param name="managedObjectRepresentation"> </param>
		/// <returns> ManagedObjectRepresentation updated ManagedObject. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation);

		/// <summary>
		/// Gets managed object resource by id. To get the managed object representation you have to call {@code get()} on the returned resource.
		/// </summary>
		/// <param name="gid"> id of the managed object to search for </param>
		/// <returns> the managed object resource associated with the given id </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IManagedObject getManagedObjectApi(GId gid);

		/// <summary>
		/// Creates managed object in the platform. The id of the managed object must not be set, since it will be generated by the platform
		/// </summary>
		/// <param name="managedObject"> the managed object to be created </param>
		/// <returns> the created managed object with the generated id </returns>
		/// <exception cref="SDKException"> if the managed object could not be created </exception>
		ManagedObjectRepresentation create(ManagedObjectRepresentation managedObject);

		/// <summary>
		/// Gets the all the managed object in the platform
		/// </summary>
		/// <returns> collection of managed objects with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IManagedObjectCollection ManagedObjects { get; }

		/// <summary>
		/// Gets the managed objects from the platform based on specified filter. Query based on {@code type} and {@code fragmentType} is
		/// not supported.
		/// </summary>
		/// <param name="filter"> the filter criteria(s) </param>
		/// <returns> collection of managed objects matched by the filter with paging functionality </returns>
		/// <exception cref="SDKException">             if the query failed </exception>
		/// <exception cref="IllegalArgumentException"> if both {@code type} and {@code fragmentType} are specified in the filter </exception>
		IManagedObjectCollection getManagedObjectsByFilter(InventoryFilter filter);

		/// <summary>
		/// Gets the managed objects from the platform based on the given ids
		/// </summary>
		/// <param name="ids"> the list of ids of the managed objects to search for </param>
		/// <returns> collection of managed objects matched in order of the given ids </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		[Obsolete]
		IManagedObjectCollection getManagedObjectsByListOfIds(IList<GId> ids);

		/// <summary>
		/// Gets managed object resource by id. To get the managed object representation you have to call {@code get()} on the returned resource.
		/// </summary>
		/// <param name="gid"> id of the managed object to search for </param>
		/// <returns> the managed object resource associated with the given id </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		[Obsolete]
		IManagedObject getManagedObject(GId gid);
	}
}