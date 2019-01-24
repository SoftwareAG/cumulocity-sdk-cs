using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Inventory
{
	public class InventoryRepresentation : AbstractExtensibleRepresentation
	{
		/// <summary>
		///     The Managed Objects reference
		/// </summary>
		private ManagedObjectReferenceCollectionRepresentation managedObjects;

		/// <summary>
		///     The URL to get Managed Objects by fragment type
		/// </summary>
		private string managedObjectsForFragmentType;

		/// <summary>
		///     The URL to get Managed Objects for a given list of ids
		/// </summary>
		private string managedObjectsForListOfIds;

		/// <summary>
		///     The URL to get Managed Objects by type
		/// </summary>
		private string managedObjectsForType;

		/// <returns> the managedObjectsForType </returns>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ManagedObjectsForType
		{
			get => managedObjectsForType;
			set => managedObjectsForType = value;
		}

		/// <returns> the managedObjectsForFragmentType </returns>
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ManagedObjectsForFragmentType
		{
			get => managedObjectsForFragmentType;
			set => managedObjectsForFragmentType = value;
		}

		/// <returns> the managedObjects </returns>
		public ManagedObjectReferenceCollectionRepresentation ManagedObjects
		{
			get => managedObjects;
			set => managedObjects = value;
		}

		/// <returns> managedObjectsForListOfIds </returns>
		public virtual string ManagedObjectsForListOfIds
		{
			get => managedObjectsForListOfIds;
			set => managedObjectsForListOfIds = value;
		}
	}
}