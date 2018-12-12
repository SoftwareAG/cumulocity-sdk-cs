using System;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
public class ManagedObjectImpl : IManagedObject
{
	internal const string PAGE_SIZE_PARAM_WITH_MAX_VALUE = "?pageSize=32767";
	private readonly RestConnector restConnector;
	private readonly int pageSize;
	private volatile ManagedObjectRepresentation managedObject;
	internal readonly string url;

	public ManagedObjectImpl(RestConnector restConnector, string url, int pageSize)
	{
		this.restConnector = restConnector;
		this.url = url;
		this.pageSize = pageSize;
	}

	/// @deprecated 
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Deprecated public ManagedObjectRepresentation get() throws SDKException
	[Obsolete]
	public virtual ManagedObjectRepresentation get()
	{
		return this.restConnector.Get<ManagedObjectRepresentation>(this.url, InventoryMediaType.GetInstance.MANAGED_OBJECT, typeof(ManagedObjectRepresentation));
	}

	/// @deprecated 
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Deprecated public void delete() throws SDKException
	[Obsolete]
	public virtual void delete()
	{
		this.restConnector.Delete(this.url);
	}

	/// @deprecated 
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Deprecated public ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation) throws SDKException
	[Obsolete]
	public virtual ManagedObjectRepresentation update(ManagedObjectRepresentation managedObjectRepresentation)
	{
		return (ManagedObjectRepresentation)this.restConnector.Put(this.url, InventoryMediaType.GetInstance.MANAGED_OBJECT, managedObjectRepresentation);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String createChildDevicePath() throws SDKException
	private string createChildDevicePath()
	{
		ManagedObjectRepresentation managedObjectRepresentation = this.Internal;
		if (managedObjectRepresentation != null && managedObjectRepresentation.ChildDevices != null)
		{
			return managedObjectRepresentation.ChildDevices.Self;
		}
		else
		{
			throw new SDKException("Unable to get the child device URL");
		}
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceCollection getChildDevices() throws SDKException
	public virtual IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildDevices
	{
		get
		{
			string self = this.createChildDevicePath();
			return new ManagedObjectReferenceCollectionImpl<ManagedObjectReferenceCollectionRepresentation>(this.restConnector, self, this.pageSize);
		}
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation addChildDevice(ManagedObjectReferenceRepresentation refrenceReprsentation) throws SDKException
	public virtual ManagedObjectReferenceRepresentation addChildDevice(ManagedObjectReferenceRepresentation refrenceReprsentation)
	{
		return (ManagedObjectReferenceRepresentation)this.restConnector.Post(this.createChildDevicePath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, refrenceReprsentation);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation addChildDevice(GId childId) throws SDKException
	public virtual ManagedObjectReferenceRepresentation addChildDevice(GId childId)
	{
		return (ManagedObjectReferenceRepresentation)this.restConnector.Post(this.createChildDevicePath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, this.createChildObject(childId));
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectRepresentation addChildDevice(ManagedObjectRepresentation representation) throws SDKException
	public virtual ManagedObjectRepresentation addChildDevice(ManagedObjectRepresentation representation)
	{
		return (ManagedObjectRepresentation)this.restConnector.Post(this.createChildDevicePath(), InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation getChildDevice(GId deviceId) throws SDKException
	public virtual ManagedObjectReferenceRepresentation getChildDevice(GId deviceId)
	{
		string path = this.createChildDevicePath() + "/" + deviceId.Value;
		return this.restConnector.Get<ManagedObjectReferenceRepresentation>(path, InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, typeof(ManagedObjectReferenceRepresentation));
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void deleteChildDevice(GId deviceId) throws SDKException
	public virtual void deleteChildDevice(GId deviceId)
	{
		string path = this.createChildDevicePath() + "/" + deviceId.Value;
		this.restConnector.Delete(path);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation addChildAssets(ManagedObjectReferenceRepresentation refrenceReprsentation) throws SDKException
	public virtual ManagedObjectReferenceRepresentation addChildAssets(ManagedObjectReferenceRepresentation refrenceReprsentation)
	{
		return (ManagedObjectReferenceRepresentation)this.restConnector.Post(this.createChildAssetPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, refrenceReprsentation);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation addChildAssets(GId childId) throws SDKException
	public virtual ManagedObjectReferenceRepresentation addChildAssets(GId childId)
	{
		return (ManagedObjectReferenceRepresentation)this.restConnector.Post(this.createChildAssetPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, this.createChildObject(childId));
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectRepresentation addChildAsset(ManagedObjectRepresentation representation) throws SDKException
	public virtual ManagedObjectRepresentation addChildAsset(ManagedObjectRepresentation representation)
	{
		return (ManagedObjectRepresentation)this.restConnector.Post(this.createChildAssetPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
	}

	private ManagedObjectReferenceRepresentation createChildObject(GId childId)
	{
		ManagedObjectReferenceRepresentation morr = new ManagedObjectReferenceRepresentation();
		ManagedObjectRepresentation mor = new ManagedObjectRepresentation();
		mor.Id = childId;
		morr.ManagedObject = mor;
		return morr;
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String createChildAssetPath() throws SDKException
	private string createChildAssetPath()
	{
		ManagedObjectRepresentation managedObjectRepresentation = this.Internal;
		if (managedObjectRepresentation != null && managedObjectRepresentation.ChildAssets != null)
		{
			return managedObjectRepresentation.ChildAssets.Self;
		}
		else
		{
			throw new SDKException("Unable to get the child device URL");
		}
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceCollection getChildAssets() throws SDKException
	public virtual IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAssets
	{
		get
		{
			string self = this.createChildAssetPath();
			return new ManagedObjectReferenceCollectionImpl<ManagedObjectReferenceCollectionRepresentation>(this.restConnector, self, this.pageSize);
		}
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation getChildAsset(GId assetId) throws SDKException
	public virtual ManagedObjectReferenceRepresentation getChildAsset(GId assetId)
	{
		string path = this.createChildAssetPath() + "/" + assetId.Value;
		return this.restConnector.Get<ManagedObjectReferenceRepresentation>(path, InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, typeof(ManagedObjectReferenceRepresentation));
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void deleteChildAsset(GId assetId) throws SDKException
	public virtual void deleteChildAsset(GId assetId)
	{
		string path = this.createChildAssetPath() + "/" + assetId.Value;
		this.restConnector.Delete(path);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation addChildAdditions(ManagedObjectReferenceRepresentation refrenceReprsentation) throws SDKException
	public virtual ManagedObjectReferenceRepresentation addChildAdditions(ManagedObjectReferenceRepresentation refrenceReprsentation)
	{
		return (ManagedObjectReferenceRepresentation)this.restConnector.Post(this.createChildAdditionPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, refrenceReprsentation);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation addChildAdditions(GId childId) throws SDKException
	public virtual ManagedObjectReferenceRepresentation addChildAdditions(GId childId)
	{
		return (ManagedObjectReferenceRepresentation)this.restConnector.Post(this.createChildAdditionPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, this.createChildObject(childId));
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectRepresentation addChildAddition(ManagedObjectRepresentation representation) throws SDKException
	public virtual ManagedObjectRepresentation addChildAddition(ManagedObjectRepresentation representation)
	{
		return (ManagedObjectRepresentation)this.restConnector.Post(this.createChildAdditionPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private String createChildAdditionPath() throws SDKException
	private string createChildAdditionPath()
	{
		ManagedObjectRepresentation managedObjectRepresentation = this.Internal;
		if (managedObjectRepresentation != null && managedObjectRepresentation.ChildAdditions != null)
		{
			return managedObjectRepresentation.ChildAdditions.Self;
		}
		else
		{
			throw new SDKException("Unable to get the child device URL");
		}
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceCollection getChildAdditions() throws SDKException
	public virtual IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAdditions
	{
		get
		{
			string self = this.createChildAdditionPath();
			return new ManagedObjectReferenceCollectionImpl<ManagedObjectReferenceCollectionRepresentation>(this.restConnector, self, this.pageSize);
		}
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ManagedObjectReferenceRepresentation getChildAddition(GId additionId) throws SDKException
	public virtual ManagedObjectReferenceRepresentation getChildAddition(GId additionId)
	{
		string path = this.createChildAdditionPath() + "/" + additionId.Value;
		return this.restConnector.Get<ManagedObjectReferenceRepresentation>(path, InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, typeof(ManagedObjectReferenceRepresentation));
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void deleteChildAddition(GId additionId) throws SDKException
	public virtual void deleteChildAddition(GId additionId)
	{
		string path = this.createChildAdditionPath() + "/" + additionId.Value;
		this.restConnector.Delete(path);
	}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private ManagedObjectRepresentation getInternal() throws SDKException
	private ManagedObjectRepresentation Internal
	{
		get
		{
			if (this.managedObject == null)
			{
				this.managedObject = this.restConnector.Get<ManagedObjectRepresentation>(this.url, InventoryMediaType.GetInstance.MANAGED_OBJECT, typeof(ManagedObjectRepresentation));
			}
    
			return this.managedObject;
		}
	}
}

}