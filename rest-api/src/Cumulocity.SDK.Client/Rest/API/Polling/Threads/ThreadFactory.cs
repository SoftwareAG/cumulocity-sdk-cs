namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	using Cumulocity.SDK.Client.Rest.API.Polling.Threads;
	public class ThreadFactory
	{

		public Thread NewThread(Runnable r)
		{
			Thread t = new Thread(r);
			t.SetDaemon(true);
			t.Start();
			return t;
		}
	}
}