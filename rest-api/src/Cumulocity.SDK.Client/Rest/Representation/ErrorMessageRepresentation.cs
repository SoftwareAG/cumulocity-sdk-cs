using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation
{
    public class ErrorMessageRepresentation : IResourceRepresentation
    {
        // details     JSON-Object with ErrorDetails. Only available if in DEBUG mode.
        private ErrorDetails details;

        // Application level error code
        private string error;

        private string info; //   1   URL to error description on the Internet.

        // Short text description of the error
        private string message;

        public virtual string Error
        {
            get => error;
            set => error = value;
        }


        public virtual string Message
        {
            get => message;
            set => message = value;
        }


        public virtual string Info
        {
            get => info;
            set => info = value;
        }


        public virtual ErrorDetails Details
        {
            get => details;
            set => details = value;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('{');
            sb.Append("error=").Append('"').Append(error).Append('"').Append(',');
            sb.Append("message=").Append('"').Append(message).Append('"').Append(',');
            sb.Append("info=").Append('"').Append(info).Append('"').Append(',');
            sb.Append("details=").Append('"').Append(details).Append('"');
            sb.Append('}');
            return sb.ToString();
        }
    }
}