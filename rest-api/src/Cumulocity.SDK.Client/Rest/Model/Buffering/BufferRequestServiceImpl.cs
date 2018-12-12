using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class BufferRequestServiceImpl : IBufferRequestService
    {
        //ConcurrentMap<Long, Future> futures = new ConcurrentHashMap<Long, Future>();
        private readonly ConcurrentDictionary<long?, Task> futures = new ConcurrentDictionary<long?, Task>();

        private readonly PersistentProvider persistentProvider;

        public BufferRequestServiceImpl(PersistentProvider persistentProvider)
        {
            this.persistentProvider = persistentProvider;
        }

        public virtual Task create(BufferedRequest request)
        {
            var requestId = persistentProvider.generateId();
            var future = initAsyncResponse(requestId);
            persistentProvider.offer(new ProcessingRequest(requestId, request));
            return future;
        }

        public virtual void addResponse(long requestId, Result result)
        {
            Task future;
            futures.TryGetValue(requestId, out future);
            if (future != null) futures.TryRemove(requestId, out future);
        }

        private Task initAsyncResponse(long requestId)
        {
            var future = new Task(() => { });
            futures.TryAdd(requestId, future);
            return future;
        }
    }
}