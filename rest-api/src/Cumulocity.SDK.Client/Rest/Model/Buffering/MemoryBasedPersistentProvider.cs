using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
    public class MemoryBasedPersistentProvider : PersistentProvider
    {
        private readonly BlockingQueue<ProcessingRequest> requestQueue = new BlockingQueue<ProcessingRequest>();

        private long _counter;

        public MemoryBasedPersistentProvider()
        {
        }

        public MemoryBasedPersistentProvider(long bufferLimit) : base(bufferLimit)
        {
        }

        public override long generateId()
        {
            return Interlocked.Increment(ref _counter);
        }

        public override void offer(ProcessingRequest request)
        {
            if (requestQueue.Count() >= bufferLimit) throw new InvalidOperationException("Queue is full");

            try
            {
                requestQueue.Enqueue(request);
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }

        public override ProcessingRequest poll()
        {
            try
            {
                return requestQueue.Dequeue();
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }
    }

    public class BlockingQueue<T> : IEnumerable<T>
    {
        private int _count;

        private readonly Queue<T> _queue = new Queue<T>();

        // Lets the consumer thread consume the queue with a foreach loop.
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            while (true) yield return Dequeue();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        public T Dequeue()
        {
            lock (_queue)
            {
                // If we have items remaining in the queue, skip over this. 
                while (_count <= 0)
                    // Release the lock and block on this line until someone
                    // adds something to the queue, resuming once they 
                    // release the lock again.
                    Monitor.Wait(_queue);

                _count--;

                return _queue.Dequeue();
            }
        }

        public void Enqueue(T data)
        {
            if (data == null) throw new ArgumentNullException("data");

            lock (_queue)
            {
                _queue.Enqueue(data);

                _count++;

                // If the consumer thread is waiting for an item
                // to be added to the queue, this will move it
                // to a waiting list, to resume execution
                // once we release our lock.
                Monitor.Pulse(_queue);
            }
        }

        public int Count()
        {
            lock (_queue)
            {
                return _queue.Count;
            }
        }
    }
}