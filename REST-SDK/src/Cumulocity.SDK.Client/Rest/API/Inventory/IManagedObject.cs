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
		ManagedObjectRepresentation Get();

		/// <summary>
		/// Deletes the Managed Object from the Cumulocity Server.
		/// </summary>
		/// <exception cref="SDKException"> </exception>
		[Obsolete]
		void Delete();

		/// <summary>
		/// This Update the ManagedObject for the operationCollection. Cannot Update the ID.
		/// </summary>
		/// <param name="managedObjectRepresentation"> </param>
		/// <returns> ManagedObjectRepresentation updated ManagedObject. </returns>
		/// <exception cref="SDKException"> </exception>
		[Obsolete]
		ManagedObjectRepresentation Update(ManagedObjectRepresentation managedObjectRepresentation);

		/// <summary>
		/// Adds a child device to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation AddChildDevice(ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child device to the ManagedObject.
		/// </summary>
		/// <param name="childId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation AddChildDevice(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child device to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation AddChildDevice(ManagedObjectRepresentation representation);

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
		ManagedObjectReferenceRepresentation GetChildDevice(GId deviceId);

		/// <summary>
		/// Deletes the child device  and its relation to the managed object.
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <exception cref="SDKException"> </exception>
		void DeleteChildDevice(GId deviceId);

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
		ManagedObjectReferenceRepresentation AddChildAssets(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child asset to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation AddChildAsset(ManagedObjectRepresentation representation);

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
		ManagedObjectReferenceRepresentation GetChildAsset(GId assetId);

		/// <summary>
		/// Deletes the child Asset  and its relation to the managed object.
		/// </summary>
		/// <param name="assetId"> </param>
		/// <exception cref="SDKException"> </exception>

		void DeleteChildAsset(GId assetId);

		/// <summary>
		/// Adds a child addition to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child addition. </returns>
		/// <exception cref="SDKException"> </exception>

		ManagedObjectReferenceRepresentation AddChildAdditions(
			ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child addition to the ManagedObject.
		/// </summary>
		/// <param name="childId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child addition. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectReferenceRepresentation AddChildAdditions(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child addition to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
		ManagedObjectRepresentation AddChildAddition(ManagedObjectRepresentation representation);

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
		ManagedObjectReferenceRepresentation GetChildAddition(GId additionId);

		/// <summary>
		/// Deletes the child addition and its relation to the managed object.
		/// </summary>
		/// <param name="additionId"> </param>
		/// <exception cref="SDKException"> </exception>
		void DeleteChildAddition(GId additionId);
	}
}