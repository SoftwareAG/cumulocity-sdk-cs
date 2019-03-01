namespace Cumulocity.SDK.Client.Rest.Representation.Audit
{
	public interface IAuditLogSource<ID>
	{
		ID LogSource { get; }
	}
}