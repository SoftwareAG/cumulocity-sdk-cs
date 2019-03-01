namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	public interface IGetResultTask<K>
	{
		K TryGetResult();
	}
}