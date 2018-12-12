namespace Cumulocity.SDK.Client.HelperTest
{
	public interface ISecretRevealer
	{
		(string user, string pass) Reveal();
	}
}
