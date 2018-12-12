using System.Net.Http;

namespace Cumulocity.SDK.Client.Rest.Utils
{
    internal static class DelegatingHandlerExtensions
    {
        public static HttpMessageHandler DecorateWith(this HttpMessageHandler current, DelegatingHandler handler)
        {
            handler.InnerHandler = current;
            return handler;
        }

        public static HttpMessageHandler DecorateWith(this HttpMessageHandler current, DelegatingHandler handler, bool decorate)
        {
            if (decorate)
            {
                handler.InnerHandler = current;
                return handler;
            }
            return current;
        }
    }
}