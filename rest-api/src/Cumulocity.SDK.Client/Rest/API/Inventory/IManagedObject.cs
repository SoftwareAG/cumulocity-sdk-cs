using System;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
	public interface IManagedObject
	{

		/// <summary>
		/// Returns the Managed Object of the Resource.
		/// </summary>
		/// <returns> ManagedObjectRepresentation </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Deprecated ManagedObjectRepresentation get() throws SDKException;
		[Obsolete]
		ManagedObjectRepresentation get();

		/// <summary>
		/// Deletes the Managed Object from the Cumulocity Server.
		/// </summary>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Deprecated void delete() throws SDKException;
		[Obsolete]
		void delete();

		/// <summary>
		/// This update the ManagedObject for the operationCollection. Cannot update the ID.
		/// </summary>
		/// <param name="managedObjectRepresentation"> </param>
		/// <returns> ManagedObjectRepresentation updated ManagedObject. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Deprecated ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation) throws SDKException;
		[Obsolete]
		ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation);

		/// <summary>
		/// Adds a child device to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation addChildDevice(ManagedObjectReferenceRepresentation refrenceReprsentation) throws SDKException;
		ManagedObjectReferenceRepresentation addChildDevice(ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child device to the ManagedObject.
		/// </summary>
		/// <param name="childId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation addChildDevice(GId childId) throws SDKException;
		ManagedObjectReferenceRepresentation addChildDevice(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child device to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectRepresentation addChildDevice(ManagedObjectRepresentation representation) throws SDKException;
		ManagedObjectRepresentation addChildDevice(ManagedObjectRepresentation representation);

		/// <summary>
		/// Returns all the child Devices for the Managed Object in paged collection form.
		/// </summary>
		/// <returns> ManagedObjectReferenceCollectionRepresentation which contains all the child devices. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceCollection getChildDevices() throws SDKException;
		IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildDevices { get; }

		/// <summary>
		/// Returns the child device with the given id. If it belongs to the ManagedObject.
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation of the child device. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation getChildDevice(GId deviceId) throws SDKException;
		ManagedObjectReferenceRepresentation getChildDevice(GId deviceId);

		/// <summary>
		/// Deletes the child device  and its relation to the managed object.
		/// </summary>
		/// <param name="deviceId"> </param>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void deleteChildDevice(GId deviceId) throws SDKException;
		void deleteChildDevice(GId deviceId);

		/// <summary>
		/// Adds a child asset to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation addChildAssets(ManagedObjectReferenceRepresentation refrenceReprsentation) throws SDKException;
		ManagedObjectReferenceRepresentation addChildAssets(ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child asset to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child device. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation addChildAssets(GId childId) throws SDKException;
		ManagedObjectReferenceRepresentation addChildAssets(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child asset to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectRepresentation addChildAsset(ManagedObjectRepresentation representation) throws SDKException;
		ManagedObjectRepresentation addChildAsset(ManagedObjectRepresentation representation);

		/// <summary>
		/// Returns all the child Assets for the Managed Object  in paged collection form
		/// </summary>
		/// <returns> ManagedObjectReferenceCollectionRepresentation which contains all the child devices. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceCollection getChildAssets() throws SDKException;
		IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAssets { get; }

		/// <summary>
		/// Returns the child Asset with the given id. If it belongs to the ManagedObject.
		/// </summary>
		/// <param name="assetId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation of the child device. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation getChildAsset(GId assetId) throws SDKException;
		ManagedObjectReferenceRepresentation getChildAsset(GId assetId);

		/// <summary>
		/// Deletes the child Asset  and its relation to the managed object.
		/// </summary>
		/// <param name="assetId"> </param>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void deleteChildAsset(GId assetId) throws SDKException;
		void deleteChildAsset(GId assetId);

		/// <summary>
		/// Adds a child addition to the ManagedObject.
		/// </summary>
		/// <param name="refrenceReprsentation"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child addition. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation addChildAdditions(ManagedObjectReferenceRepresentation refrenceReprsentation) throws SDKException;
		ManagedObjectReferenceRepresentation addChildAdditions(
			ManagedObjectReferenceRepresentation refrenceReprsentation);

		/// <summary>
		/// Adds a child addition to the ManagedObject.
		/// </summary>
		/// <param name="childId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation with the id of th child addition. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation addChildAdditions(GId childId) throws SDKException;
		ManagedObjectReferenceRepresentation addChildAdditions(GId childId);

		/// <summary>
		/// Create ManagedObject and adds as child addition to the parent ManagedObject.
		/// </summary>
		/// <param name="representation"> </param>
		/// <returns> ManagedObjectRepresentation with the managed object. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectRepresentation addChildAddition(ManagedObjectRepresentation representation) throws SDKException;
		ManagedObjectRepresentation addChildAddition(ManagedObjectRepresentation representation);

		/// <summary>
		/// Returns all the child additions for the Managed Object in paged collection form
		/// </summary>
		/// <returns> ManagedObjectReferenceCollectionRepresentation which contains all the child additions. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceCollection getChildAdditions() throws SDKException;
		IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAdditions { get; }

		/// <summary>
		/// Returns the child additions with the given id. If it belongs to the ManagedObject.
		/// </summary>
		/// <param name="additionId"> </param>
		/// <returns> ManagedObjectReferenceRepresentation of the child additions. </returns>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: ManagedObjectReferenceRepresentation getChildAddition(GId additionId) throws SDKException;
		ManagedObjectReferenceRepresentation getChildAddition(GId additionId);

		/// <summary>
		/// Deletes the child addition and its relation to the managed object.
		/// </summary>
		/// <param name="additionId"> </param>
		/// <exception cref="SDKException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void deleteChildAddition(GId additionId) throws SDKException;
		void deleteChildAddition(GId additionId);
	}
}