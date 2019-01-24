namespace Cumulocity.SDK.Client.Rest.Representation.Identity
{
	public class IdentityRepresentation : AbstractExtensibleRepresentation
	{
		public virtual string ExternalId { get; set; }

		public virtual string ExternalIdsOfGlobalId { get; set; }
	}
}