using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Inventory
{
	public class InventoryMediaType : CumulocityMediaType
	{
		public readonly InventoryMediaType MANAGED_OBJECT;
		public readonly InventoryMediaType MANAGED_OBJECT_COLLECTION;
		public readonly InventoryMediaType MANAGED_OBJECT_REFERENCE;
		public readonly InventoryMediaType MANAGED_OBJECT_REFERENCE_COLLECTION;
		public readonly InventoryMediaType INVENTORY_API;
		public readonly InventoryMediaType MANAGED_OBJECT_USER;
		public readonly string MANAGED_OBJECT_TYPE;
		public readonly string MANAGED_OBJECT_COLLECTION_TYPE;
		public readonly string MANAGED_OBJECT_REFERENCE_TYPE;
		public readonly string MANAGED_OBJECT_REFERENCE_COLLECTION_TYPE;
		public readonly string INVENTORY_API_TYPE;
		public readonly string MANAGED_OBJECT_USER_TYPE;

		public InventoryMediaType(string @string) : base(@string)
		{
		}

		private static InventoryMediaType instance;
		private static object syncRoot = new Object();

		public InventoryMediaType()
		{
			this.MANAGED_OBJECT = new InventoryMediaType("managedObject");
			this.MANAGED_OBJECT_COLLECTION = new InventoryMediaType("managedObjectCollection");
			this.MANAGED_OBJECT_REFERENCE = new InventoryMediaType("managedObjectReference");
			this.MANAGED_OBJECT_REFERENCE_COLLECTION = new InventoryMediaType("managedObjectReferenceCollection");
			this.INVENTORY_API = new InventoryMediaType("inventoryApi");
			this.MANAGED_OBJECT_USER = new InventoryMediaType("managedObjectUser");
			this.MANAGED_OBJECT_TYPE = "application/vnd.com.nsn.cumulocity.managedObject+json;charset=UTF-8;ver=0.9";
			this.MANAGED_OBJECT_COLLECTION_TYPE = "application/vnd.com.nsn.cumulocity.managedObjectCollection+json;charset=UTF-8;ver=0.9";
			this.MANAGED_OBJECT_REFERENCE_TYPE = "application/vnd.com.nsn.cumulocity.managedObjectReference+json;charset=UTF-8;ver=0.9";
			this.MANAGED_OBJECT_REFERENCE_COLLECTION_TYPE = "application/vnd.com.nsn.cumulocity.managedObjectReferenceCollection+json;charset=UTF-8;ver=0.9";
			this.INVENTORY_API_TYPE = "application/vnd.com.nsn.cumulocity.inventoryApi+json;charset=UTF-8;ver=0.9";
			this.MANAGED_OBJECT_USER_TYPE = "application/vnd.com.nsn.cumulocity.managedObjectUser+json;charset=UTF-8;ver=0.9";
		}

		public static InventoryMediaType GetInstance
		{
			get
			{
				lock (syncRoot)
				{
					if (instance == null)
					{
						instance = new InventoryMediaType();
					}
				}

				return instance;
			}
		}
	}
}