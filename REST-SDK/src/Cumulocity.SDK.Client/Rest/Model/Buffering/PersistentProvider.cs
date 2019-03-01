namespace Cumulocity.SDK.Client.Rest.Model.Buffering
{
	public abstract class PersistentProvider
	{
		protected internal const long DEFAULT_BUFFER_LIMIT = 10000;

		protected internal long bufferLimit = DEFAULT_BUFFER_LIMIT;

		public PersistentProvider()
		{
		}

		public PersistentProvider(long bufferLimit)
		{
			this.bufferLimit = bufferLimit;
		}

		public abstract long generateId();

		public abstract void offer(ProcessingRequest request);

		public abstract ProcessingRequest poll();
	}
}