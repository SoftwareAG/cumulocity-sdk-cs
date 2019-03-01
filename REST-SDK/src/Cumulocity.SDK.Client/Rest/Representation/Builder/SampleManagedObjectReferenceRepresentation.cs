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
				RestRepresentationObjectBuilder.aManagedObjectReferenceRepresentation().WithSelf(SELF);
		}

		public static Func<ManagedObjectReferenceRepresentationBuilder> MO_REF_REPRESENTATION { get; }

		public override ManagedObjectReferenceRepresentation Build()
		{
			return Builder().Build();
		}
	}
}