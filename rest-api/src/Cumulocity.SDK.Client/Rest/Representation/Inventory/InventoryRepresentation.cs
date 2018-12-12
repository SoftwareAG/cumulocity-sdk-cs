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
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(value = "managedObjectsForType", ignoreIfNull = true) public final String getManagedObjectsForType()
        public string ManagedObjectsForType
        {
            get => managedObjectsForType;
            set => managedObjectsForType = value;
        }


        /// <returns> the managedObjectsForFragmentType </returns>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(value = "managedObjectsForFragmentType", ignoreIfNull = true) public final String getManagedObjectsForFragmentType()
        public string ManagedObjectsForFragmentType
        {
            get => managedObjectsForFragmentType;
            set => managedObjectsForFragmentType = value;
        }


        /// <returns> the managedObjects </returns>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(value = "managedObjects", ignoreIfNull = false) public final ManagedObjectReferenceCollectionRepresentation getManagedObjects()
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