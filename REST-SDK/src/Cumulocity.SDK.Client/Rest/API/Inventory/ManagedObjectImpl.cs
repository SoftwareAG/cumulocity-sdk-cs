using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;

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

		[Obsolete]
		public virtual ManagedObjectRepresentation Get()
		{
			return this.restConnector.Get<ManagedObjectRepresentation>(this.url, InventoryMediaType.GetInstance.MANAGED_OBJECT, typeof(ManagedObjectRepresentation));
		}

		[Obsolete]
		public virtual void Delete()
		{
			this.restConnector.Delete(this.url);
		}

		[Obsolete]
		public virtual ManagedObjectRepresentation Update(ManagedObjectRepresentation managedObjectRepresentation)
		{
			return this.restConnector.Put(this.url, InventoryMediaType.GetInstance.MANAGED_OBJECT, managedObjectRepresentation);
		}

		private string createChildDevicePath()
		{
			ManagedObjectRepresentation managedObjectRepresentation = this.Internal;
			if (managedObjectRepresentation != null && managedObjectRepresentation.ChildDevices != null)
			{
				return managedObjectRepresentation.ChildDevices.Self;
			}
			else
			{
				throw new SDKException("Unable to GetFirstPage the child device URL");
			}
		}

		public virtual IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildDevices
		{
			get
			{
				string self = this.createChildDevicePath();
				return new ManagedObjectReferenceCollectionImpl<ManagedObjectReferenceCollectionRepresentation>(this.restConnector, self, this.pageSize);
			}
		}

		public virtual ManagedObjectReferenceRepresentation AddChildDevice(ManagedObjectReferenceRepresentation refrenceReprsentation)
		{
			return this.restConnector.Post(this.createChildDevicePath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, refrenceReprsentation);
		}

		public virtual ManagedObjectReferenceRepresentation AddChildDevice(GId childId)
		{
			return this.restConnector.Post(this.createChildDevicePath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, this.createChildObject(childId));
		}

		public virtual ManagedObjectRepresentation AddChildDevice(ManagedObjectRepresentation representation)
		{
			return this.restConnector.Post(this.createChildDevicePath(), InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
		}

		public virtual ManagedObjectReferenceRepresentation GetChildDevice(GId deviceId)
		{
			string path = $"{this.createChildDevicePath()}/{deviceId.Value}";
			return this.restConnector.Get<ManagedObjectReferenceRepresentation>(path, InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, typeof(ManagedObjectReferenceRepresentation));
		}

		public virtual void DeleteChildDevice(GId deviceId)
		{
			string path = $"{this.createChildDevicePath()}/{deviceId.Value}";
			this.restConnector.Delete(path);
		}

		public virtual ManagedObjectReferenceRepresentation addChildAssets(ManagedObjectReferenceRepresentation refrenceReprsentation)
		{
			return this.restConnector.Post(this.createChildAssetPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, refrenceReprsentation);
		}

		public virtual ManagedObjectReferenceRepresentation AddChildAssets(GId childId)
		{
			return this.restConnector.Post(this.createChildAssetPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, this.createChildObject(childId));
		}

		public virtual ManagedObjectRepresentation AddChildAsset(ManagedObjectRepresentation representation)
		{
			return this.restConnector.Post(this.createChildAssetPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
		}

		private ManagedObjectReferenceRepresentation createChildObject(GId childId)
		{
			ManagedObjectReferenceRepresentation morr = new ManagedObjectReferenceRepresentation();
			ManagedObjectRepresentation mor = new ManagedObjectRepresentation();
			mor.Id = childId;
			morr.ManagedObject = mor;
			return morr;
		}

		private string createChildAssetPath()
		{
			ManagedObjectRepresentation managedObjectRepresentation = this.Internal;
			if (managedObjectRepresentation != null && managedObjectRepresentation.ChildAssets != null)
			{
				return managedObjectRepresentation.ChildAssets.Self;
			}
			else
			{
				throw new SDKException("Unable to GetFirstPage the child device URL");
			}
		}

		public virtual IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAssets
		{
			get
			{
				string self = this.createChildAssetPath();
				return new ManagedObjectReferenceCollectionImpl<ManagedObjectReferenceCollectionRepresentation>(this.restConnector, self, this.pageSize);
			}
		}

		public virtual ManagedObjectReferenceRepresentation GetChildAsset(GId assetId)
		{
			string path = $"{this.createChildAssetPath()}/{assetId.Value}";
			return this.restConnector.Get<ManagedObjectReferenceRepresentation>(path, InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, typeof(ManagedObjectReferenceRepresentation));
		}

		public virtual void DeleteChildAsset(GId assetId)
		{
			string path = $"{this.createChildAssetPath()}/{assetId.Value}";
			this.restConnector.Delete(path);
		}

		public virtual ManagedObjectReferenceRepresentation AddChildAdditions(ManagedObjectReferenceRepresentation refrenceReprsentation)
		{
			return this.restConnector.Post(this.createChildAdditionPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, refrenceReprsentation);
		}

		public virtual ManagedObjectReferenceRepresentation AddChildAdditions(GId childId)
		{
			return this.restConnector.Post(this.createChildAdditionPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, this.createChildObject(childId));
		}

		public virtual ManagedObjectRepresentation AddChildAddition(ManagedObjectRepresentation representation)
		{
			return this.restConnector.Post(this.createChildAdditionPath(), InventoryMediaType.GetInstance.MANAGED_OBJECT, representation);
		}

		private string createChildAdditionPath()
		{
			ManagedObjectRepresentation managedObjectRepresentation = this.Internal;
			if (managedObjectRepresentation != null && managedObjectRepresentation.ChildAdditions != null)
			{
				return managedObjectRepresentation.ChildAdditions.Self;
			}
			else
			{
				throw new SDKException("Unable to GetFirstPage the child device URL");
			}
		}

		public virtual IManagedObjectReferenceCollection<ManagedObjectReferenceCollectionRepresentation> ChildAdditions
		{
			get
			{
				string self = this.createChildAdditionPath();
				return new ManagedObjectReferenceCollectionImpl<ManagedObjectReferenceCollectionRepresentation>(this.restConnector, self, this.pageSize);
			}
		}

		public virtual ManagedObjectReferenceRepresentation GetChildAddition(GId additionId)
		{
			string path = $"{this.createChildAdditionPath()}/{additionId.Value}";
			return this.restConnector.Get<ManagedObjectReferenceRepresentation>(path, InventoryMediaType.GetInstance.MANAGED_OBJECT_REFERENCE, typeof(ManagedObjectReferenceRepresentation));
		}

		public virtual void DeleteChildAddition(GId additionId)
		{
			string path = $"{this.createChildAdditionPath()}/{additionId.Value}";
			this.restConnector.Delete(path);
		}

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