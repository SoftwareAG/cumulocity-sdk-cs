using Cumulocity.SDK.Client.Rest.Model;
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

		public virtual ManagedObjectRepresentationBuilder WithName(string value)
		{
			setFieldValue("name", value);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithID(ID id)
		{
			setFieldValue("id", id);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithType(string type)
		{
			setFieldValue("type", type);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithChildAsset(ID childAsset)
		{
			appendToSet(childAsset, "childAssets");
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithChildAssets(ManagedObjectReferenceCollectionRepresentation childAssets)
		{
			setFieldValue("childAssets", childAssets);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithChildDevice(ID childDevice)
		{
			appendToSet(childDevice, "childDevices");
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithChildDevices(ManagedObjectReferenceCollectionRepresentation childDevices)
		{
			setFieldValue("childDevices", childDevices);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithChildAddition(ID childAddition)
		{
			appendToSet(childAddition, "childAdditions");
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithChildAdditions(ManagedObjectReferenceCollectionRepresentation childAdditions)
		{
			setFieldValue("childAdditions", childAdditions);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithParent(ID parent)
		{
			appendToSet(parent, "parents");
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithParents(ManagedObjectReferenceCollectionRepresentation parents)
		{
			setFieldValue("parents", parents);
			return this;
		}

		[Obsolete]
		public virtual ManagedObjectRepresentationBuilder WithLastUpdated(DateTime lastUpdated)
		{
			setFieldValue("lastUpdated", lastUpdated);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithLastUpdatedDateTime(DateTime lastUpdated)
		{
			setFieldValue("lastUpdated", lastUpdated);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder WithOwner(string owner)
		{
			setFieldValue("owner", owner);
			return this;
		}

		public virtual ManagedObjectRepresentationBuilder With(object @object)
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

		private void appendToSet(ID item, string setName)
		{
			ISet<ID> items = (null != getFieldValue(setName)) ? (ISet<ID>)getFieldValue(setName) : new System.Collections.Generic.HashSet<ID>();
			items.Add(item);
			setFieldValue(setName, items);
		}
	}
}