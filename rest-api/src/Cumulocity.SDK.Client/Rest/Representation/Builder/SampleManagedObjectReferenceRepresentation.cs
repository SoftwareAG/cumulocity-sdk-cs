using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
	public abstract class
		SampleManagedObjectReferenceRepresentation : AbstractSampleManagedObjectReferenceRepresentation
	{
		public const string SELF = "SELF_LINK";

		static SampleManagedObjectReferenceRepresentation()
		{
			MO_REF_REPRESENTATION = () =>
				RestRepresentationObjectBuilder.aManagedObjectReferenceRepresentation().withSelf(SELF);
		}

		public static Func<ManagedObjectReferenceRepresentationBuilder> MO_REF_REPRESENTATION { get; }

		public ManagedObjectReferenceRepresentation build()
		{
			return builder().build();
		}
	}
}