using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public interface IBufferRequestService
    {
        Task<object> create(BufferedRequest request);

        void addResponse(long requestId, object result);
    }
}