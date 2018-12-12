using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation
{
    public class ErrorDetails
    {
        //  Exception message content.
        private string exceptionMessage;

        //    Class name of an exception that caused this error.
        private string expectionClass;

        private string expectionStackTrace;

        public virtual string ExpectionClass
        {
            get => expectionClass;
            set => expectionClass = value;
        }


        public virtual string ExceptionMessage
        {
            get => exceptionMessage;
            set => exceptionMessage = value;
        }


        public virtual string ExpectionStackTrace
        {
            get => expectionStackTrace;
            set => expectionStackTrace = value;
        }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('{');
            sb.Append("exceptionMessage=").Append('"').Append(exceptionMessage).Append('"').Append(',');
            sb.Append("expectionClass=").Append('"').Append(expectionClass).Append('"').Append(',');
            sb.Append("expectionStackTrace=").Append('"').Append(expectionStackTrace).Append('"');
            sb.Append('}');
            return sb.ToString();
        }
    }
}