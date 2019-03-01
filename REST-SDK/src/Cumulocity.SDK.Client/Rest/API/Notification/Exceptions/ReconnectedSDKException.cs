namespace Cumulocity.SDK.Client.Rest.API.Notification.Exceptions
{
	internal class ReconnectedSDKException : SDKException
	{
		public const int UNKNOWN_CLIENT = 402;

		public ReconnectedSDKException(string message) : base(UNKNOWN_CLIENT, message)
		{
		}
	}
}