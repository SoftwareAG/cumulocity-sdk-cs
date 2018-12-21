using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Cumulocity.SDK.Client.Rest.API.Polling.Threads
{
	public class CountDownLatch
	{
		private int m_remain;
		private EventWaitHandle m_event;

		public CountDownLatch(int count)
		{
			m_remain = count;
			m_event = new ManualResetEvent(false);
		}

		public void Signal()
		{
			// The last thread to signal also sets the event.
			if (Interlocked.Decrement(ref m_remain) == 0)
				m_event.Set();
		}

		public void Wait()
		{
			m_event.WaitOne();
		}
	}
}
