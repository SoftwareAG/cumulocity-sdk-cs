using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
	public abstract class AbstractSampleManagedObjectRepresentation
	{
		public abstract ManagedObjectRepresentationBuilder builder();

		public virtual ManagedObjectRepresentation build()
		{
			return builder().build();
		}
	}
}