using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
	public interface IBufferRequestService
	{
		Task<object> Create(BufferedRequest request);

		void addResponse(long requestId, object result);
	}
}