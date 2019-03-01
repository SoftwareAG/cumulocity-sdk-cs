using System;

namespace Cumulocity.SDK.Client.Rest
{
    public class SDKException : Exception
    {
        private const long serialVersionUID = 2723464890693892731L;

        public SDKException(string @string) : base(@string)
        {
        }

        public SDKException(string @string, Exception t) : base(@string, t)
        {
        }

        public SDKException(int httpStatusCode, string @string) : base(@string)
        {
            HttpStatus = httpStatusCode;
        }

        public virtual int HttpStatus { get; } = -1;
    }
}