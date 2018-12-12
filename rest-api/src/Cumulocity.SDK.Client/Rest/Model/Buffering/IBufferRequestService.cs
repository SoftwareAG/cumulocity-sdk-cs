using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public interface IBufferRequestService
    {
        Task create(BufferedRequest request);

        void addResponse(long requestId, Result result);
    }
}