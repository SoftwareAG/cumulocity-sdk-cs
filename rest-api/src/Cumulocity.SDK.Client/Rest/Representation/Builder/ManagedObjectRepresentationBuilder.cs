using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
using System;
using System.Collections.Generic;

public class ManagedObjectRepresentationBuilder : AbstractObjectBuilder<ManagedObjectRepresentation>
{

	public static ManagedObjectRepresentationBuilder aManagedObjectRepresentation()
	{
		return new ManagedObjectRepresentationBuilder();
	}

	private List<object> dynamicProperties = new List<object>();
	
	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withName(final String value)
	public virtual ManagedObjectRepresentationBuilder withName(string value)
	{
		setFieldValue("name", value);
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withID(final ID id)
	public virtual ManagedObjectRepresentationBuilder withID(ID id)
	{
		setFieldValue("id", id);
		return this;
	}
	//Builder
	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withID(final IDBuilder id)
//	public virtual ManagedObjectRepresentationBuilder withID(IDBuilder2 id)
//	{
//		setFieldValueBuilder("id",id);
//		return this;
//	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withType(final String type)
	public virtual ManagedObjectRepresentationBuilder withType(string type)
	{
		setFieldValue("type", type);
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withChildAsset(final ID childAsset)
	public virtual ManagedObjectRepresentationBuilder withChildAsset(ID childAsset)
	{
		appendToSet(childAsset, "childAssets");
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withChildAssets(final ManagedObjectReferenceCollectionRepresentation childAssets)
	public virtual ManagedObjectRepresentationBuilder withChildAssets(ManagedObjectReferenceCollectionRepresentation childAssets)
	{
		setFieldValue("childAssets", childAssets);
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withChildDevice(final ID childDevice)
	public virtual ManagedObjectRepresentationBuilder withChildDevice(ID childDevice)
	{
		appendToSet(childDevice, "childDevices");
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withChildDevices(final ManagedObjectReferenceCollectionRepresentation childDevices)
	public virtual ManagedObjectRepresentationBuilder withChildDevices(ManagedObjectReferenceCollectionRepresentation childDevices)
	{
		setFieldValue("childDevices", childDevices);
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withChildAddition(final ID childAddition)
	public virtual ManagedObjectRepresentationBuilder withChildAddition(ID childAddition)
	{
		appendToSet(childAddition, "childAdditions");
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withChildAdditions(final ManagedObjectReferenceCollectionRepresentation childAdditions)
	public virtual ManagedObjectRepresentationBuilder withChildAdditions(ManagedObjectReferenceCollectionRepresentation childAdditions)
	{
		setFieldValue("childAdditions", childAdditions);
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withParent(final ID parent)
	public virtual ManagedObjectRepresentationBuilder withParent(ID parent)
	{
		appendToSet(parent, "parents");
		return this;
	}


	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withParents(final ManagedObjectReferenceCollectionRepresentation parents)
	public virtual ManagedObjectRepresentationBuilder withParents(ManagedObjectReferenceCollectionRepresentation parents)
	{
		setFieldValue("parents", parents);
		return this;
	}

	//ORIGINAL LINE: @Deprecated public ManagedObjectRepresentationBuilder withLastUpdated(final Date lastUpdated)
	[Obsolete]
	public virtual ManagedObjectRepresentationBuilder withLastUpdated(DateTime lastUpdated)
	{
		setFieldValue("lastUpdated", lastUpdated);
		return this;
	}


	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withLastUpdatedDateTime(final DateTime lastUpdated)
	public virtual ManagedObjectRepresentationBuilder withLastUpdatedDateTime(DateTime lastUpdated)
	{
		setFieldValue("lastUpdated", lastUpdated);
		return this;
	}

	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder withOwner(final String owner)
	public virtual ManagedObjectRepresentationBuilder withOwner(string owner)
	{
		setFieldValue("owner", owner);
		return this;
	}


	//ORIGINAL LINE: public ManagedObjectRepresentationBuilder with(final Object object)
	public virtual ManagedObjectRepresentationBuilder with(object @object)
	{
		dynamicProperties.Add(@object);
		return this;
	}

	protected internal override ManagedObjectRepresentation createDomainObject()
	{
		ManagedObjectRepresentation rep = new ManagedObjectRepresentation();
		foreach (object @object in dynamicProperties)
		{
			rep.set(@object);
		}
		return rep;
	}

	//FIXME to use ManagedObjectReferenceCollectionRepresentation
	//ORIGINAL LINE: private void appendToSet(final ID item, final String setName)
	private void appendToSet(ID item, string setName)
	{
		//TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @SuppressWarnings("unchecked") Set<ID> items = (null != getFieldValue(setName)) ? (Set<ID>) getFieldValue(setName) : new HashSet<ID>();
		ISet<ID> items = (null != getFieldValue(setName)) ? (ISet<ID>) getFieldValue(setName) : new System.Collections.Generic.HashSet<ID>();
		items.Add(item);
		setFieldValue(setName, items);
	}
}

}