namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class ProcessingRequest
    {
        public ProcessingRequest(long id, BufferedRequest request)
        {
            Id = id;
            Entity = request;
        }

        public virtual long Id { get; }

        public virtual BufferedRequest Entity { get; }

        public static ProcessingRequest parse(string name, string content)
        {
            return new ProcessingRequest(long.Parse(name), BufferedRequest.parseCsvString(content));
        }
    }
}