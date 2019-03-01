using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
    public class RestRepresentationObjectMother
    {
        public static ManagedObjectRepresentationBuilder anMoRepresentationLike(
            Func<ManagedObjectRepresentationBuilder> sampleManagedObjectRepresentation)
        {
            return sampleManagedObjectRepresentation();
        }


        public static ManagedObjectReferenceRepresentationBuilder anMoRefRepresentationLike(
        Func<ManagedObjectReferenceRepresentationBuilder>  sampleManagedObjectReferenceRepresentation)
        {
            return sampleManagedObjectReferenceRepresentation();
        }

		public static AlarmRepresentationBuilder anAlarmRepresentationLike(Func<AlarmRepresentationBuilder> sample)
		{
			return sample();
		}

	}
}