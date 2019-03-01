namespace Cumulocity.SDK.Client.Rest.Representation
{
	public interface ICumulocityMediaType
	{
		string TypeString { get; }

		string Type { get; }

		string Subtype { get; }

		System.Collections.IDictionary Parameters { get; }
	}
}