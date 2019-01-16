using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public interface IManagedObject
	{
		/// <summary>
		/// Returns the Managed Object of the Resource.
		/// </summary>
		/// <returns> ManagedObjectRepresentation </returns>
		/// <exception cref="SDKException"> </exception>

		[Obsolete]
		ManagedObjectRepresentation get();

		/// <summary>
		/// Deletes the Managed Object from the Cumulocity Server.
		/// </summary>
		/// <exception cref="SDKException"> </exception>
		[Obsolete]
		void delete();

		/// <summary>
		/// This update the ManagedObject for the operationCollection. Cannot update the ID.
		/// </summary>
		/// <param name="managedObjectRepresentation"> </param>
		/// <returns> ManagedObjectRepresentation updated ManagedObject. </returns>
		/// <exception cref="SDKException"> </exception>
		[Obsolete]
		ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation);

		/// <summary>
		/// Adds a child device to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation addChildDevice(ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child device to the ManagedObject.
		/// </summary>
		/// <param name="childId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation addChildDevice(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child device to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation addChildDevice(ManagedObjectRepresentation representation);

		/// <summary>
		/// Returns all the child Devices for the Managed Object in paged collection form.
		/// </summary>
		/// <returns> ManagedObjectReferenceCollectionRepresentation which contains all the child devices. </returns>
		/// <exception cref="SDKException"> </exception>
		IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildDevices { get; }

		/// <summary>
		/// Returns the child device with the given id. If it belongs to the ManagedObject.
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation of the child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation getChildDevice(GId deviceId);

		/// <summary>
		/// Deletes the child device  and its relation to the managed object.
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <exception cref="SDKException"> </exception>
		void deleteChildDevice(GId deviceId);

		/// <summary>
		/// Adds a child asset to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation addChildAssets(ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child asset to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation addChildAssets(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child asset to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation addChildAsset(ManagedObjectRepresentation representation);

		/// <summary>
		/// Returns all the child Assets for the Managed Object  in paged collection form
		/// </summary>
		/// <returns> ManagedObjectReferenceCollectionRepresentation which contains all the child devices. </returns>
		/// <exception cref="SDKException"> </exception>
		IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAssets { get; }

		/// <summary>
		/// Returns the child Asset with the given id. If it belongs to the ManagedObject.
		/// </summary>
		/// <param name="assetId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation of the child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation getChildAsset(GId assetId);

		/// <summary>
		/// Deletes the child Asset  and its relation to the managed object.
		/// </summary>
		/// <param name="assetId"> </param>
		/// <exception cref="SDKException"> </exception>

		void deleteChildAsset(GId assetId);

		/// <summary>
		/// Adds a child addition to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child addition. </returns>
		/// <exception cref="SDKException"> </exception>

		ManagedObjectReferenceRepresentation addChildAdditions(
			ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child addition to the ManagedObject.
		/// </summary>
		/// <param name="childId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child addition. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation addChildAdditions(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child addition to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation addChildAddition(ManagedObjectRepresentation representation);

		/// <summary>
		/// Returns all the child additions for the Managed Object in paged collection form
		/// </summary>
		/// <returns> ManagedObjectReferenceCollectionRepresentation which contains all the child additions. </returns>
		/// <exception cref="SDKException"> </exception>
		IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAdditions { get; }

		/// <summary>
		/// Returns the child additions with the given id. If it belongs to the ManagedObject.
		/// </summary>
		/// <param name="additionId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation of the child additions. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation getChildAddition(GId additionId);

		/// <summary>
		/// Deletes the child addition and its relation to the managed object.
		/// </summary>
		/// <param name="additionId"> </param>
		/// <exception cref="SDKException"> </exception>
		void deleteChildAddition(GId additionId);
	}
}