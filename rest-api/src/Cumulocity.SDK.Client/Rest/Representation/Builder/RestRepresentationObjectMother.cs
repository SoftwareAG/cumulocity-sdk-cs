using System;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
    public class RestRepresentationObjectMother
    {
        //ORIGINAL LINE: public static ManagedObjectRepresentationBuilder anMoRepresentationLike(final SampleManagedObjectRepresentation sampleManagedObjectRepresentation)
        public static ManagedObjectRepresentationBuilder anMoRepresentationLike(
            Func<ManagedObjectRepresentationBuilder> sampleManagedObjectRepresentation)
        {
            return sampleManagedObjectRepresentation();
        }


      //ORIGINAL LINE: public static ManagedObjectReferenceRepresentationBuilder anMoRefRepresentationLike(final SampleManagedObjectReferenceRepresentation sample)
        public static ManagedObjectReferenceRepresentationBuilder anMoRefRepresentationLike(
        Func<ManagedObjectReferenceRepresentationBuilder>  sampleManagedObjectReferenceRepresentation)
        {
            return sampleManagedObjectReferenceRepresentation();
        }
//
//
//        //ORIGINAL LINE: public static EventRepresentationBuilder anEventRepresentationLike(final SampleEventRepresentation sample)
//        public static EventRepresentationBuilder anEventRepresentationLike(SampleEventRepresentation sample)
//        {
//            return sample.builder();
//        }
//
//
//        //ORIGINAL LINE: public static AlarmRepresentationBuilder anAlarmRepresentationLike(final SampleAlarmRepresentation sample)
//        public static AlarmRepresentationBuilder anAlarmRepresentationLike(SampleAlarmRepresentation sample)
//        {
//            return sample.builder();
//        }
//
//
//        //ORIGINAL LINE: public static MeasurementRepresentationBuilder aMeasurementRepresentationLike(final SampleMeasurementRepresentation measurment)
//        public static MeasurementRepresentationBuilder aMeasurementRepresentationLike(
//            SampleMeasurementRepresentation measurment)
//        {
//            return measurment.builder();
//        }
//
//
//        //ORIGINAL LINE: public static BulkOperationRepresentationBuilder aBulkOperationRepresentationLike(final SampleBulkOperationRepresentation bulkOperation)
//        public static BulkOperationRepresentationBuilder aBulkOperationRepresentationLike(
//            SampleBulkOperationRepresentation bulkOperation)
//        {
//            return bulkOperation.builder();
//        }
//
//
//        //ORIGINAL LINE: public static OperationRepresentationBuilder anOperationRepresentationLike(final SampleOperationRepresentation operationRepresentation)
//        public static OperationRepresentationBuilder anOperationRepresentationLike(
//            SampleOperationRepresentation operationRepresentation)
//        {
//            return operationRepresentation.builder();
//        }
    }
}