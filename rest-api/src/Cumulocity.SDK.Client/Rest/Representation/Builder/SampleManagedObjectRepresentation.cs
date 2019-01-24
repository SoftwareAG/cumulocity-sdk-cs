using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
	public abstract class SampleManagedObjectRepresentation : AbstractSampleManagedObjectRepresentation
	{
		public const string MANAGED_OBJECT_NAME = "ManagedObject Name #";

		public const string MANAGED_OBJECT_TYPE = "ManagedObject Type #";

		public const string MANAGED_OBJECT_OWNER = "ManagedObject Owner #";

		static SampleManagedObjectRepresentation()
		{
			MO_REPRESENTATION = () =>
				RestRepresentationObjectBuilder.aManagedObjectRepresentation()
					.withName(MANAGED_OBJECT_NAME)
					.withType(MANAGED_OBJECT_TYPE);
		}

		public static Func<ManagedObjectRepresentationBuilder> MO_REPRESENTATION { get; }

		public virtual ManagedObjectRepresentation build()
		{
			return builder().build();
		}
	}
}