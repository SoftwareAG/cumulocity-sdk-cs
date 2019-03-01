namespace Cumulocity.SDK.Client.HelperTest
{
	public interface ISecretRevealer
	{
		(string user, string pass, string userbootstrap, string passbootstrap, string platformurl) Reveal();
	}
}
