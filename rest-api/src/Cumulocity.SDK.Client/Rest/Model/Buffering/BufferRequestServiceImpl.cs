using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
	public class BufferRequestServiceImpl : IBufferRequestService
	{
		private readonly ConcurrentDictionary<long, TaskCompletionSource<object>> _queue = new ConcurrentDictionary<long, TaskCompletionSource<object>>();

		private readonly PersistentProvider persistentProvider;

		public BufferRequestServiceImpl(PersistentProvider persistentProvider)
		{
			this.persistentProvider = persistentProvider;
		}

		public virtual Task<object> create(BufferedRequest request)
		{
			var tcs = new TaskCompletionSource<object>();
			var requestId = persistentProvider.generateId();
			persistentProvider.offer(new ProcessingRequest(requestId, request));
			_queue.TryAdd(requestId, tcs);
			return tcs.Task;
		}

		public virtual void addResponse(long requestId, object result)
		{
			_queue.TryGetValue(requestId, out TaskCompletionSource<object> future);
			if (future != null)
			{
				future.SetResult(result);
				_queue.TryRemove(requestId, out future);
			}
		}
	}
}