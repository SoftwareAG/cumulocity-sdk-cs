using Cumulocity.SDK.Client.Logging;
using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Watchers
{
	public class ConnectionHeartBeatWatcher : EndlessTask
	{
		public const int HEARTBEAT_INTERVAL = 12;
		private readonly int delayMin;
		private readonly int initialDelayMin;
		private readonly ICollection<IConnectionIdleListener> listeners = new ThreadSafeList<IConnectionIdleListener>();
		private DateTime lastHeartBeat = new DateTime();
		private static readonly ILog LOG = LogProvider.For<ConnectionHeartBeatWatcher>();

		public ConnectionHeartBeatWatcher(int initialDelayMin, int delayMin) : base(delayMin * 60)
		{
			this.initialDelayMin = initialDelayMin * 60;
			this.delayMin = delayMin * 60;

			HeartBeat();
		}

		public virtual void AddConnectionListener(IConnectionIdleListener listener)
		{
			listeners.Add(listener);
		}

		public virtual void HeartBeat()
		{
			lastHeartBeat = DateTime.Now;
		}

		public virtual void RemoveConnectionListener(IConnectionIdleListener listener)
		{
			listeners.Remove(listener);
		}

		public virtual void Start()
		{
			StartRun(this.initialDelayMin);
		}

		public virtual void stop()
		{
			StopRun();
		}

		protected override void ExecutionCore(CancellationToken cancellationToken)
		{
			if (IsExeededInterval())
			{
				onConnectionIdle();
			}
			else
			{
				onConnectionActive();
			}
		}

		private bool IsExeededInterval()
		{
			LOG.Debug("->IsExeededInterval");
			LOG.Debug("DateTime.Now" + DateTime.Now.ToLongTimeString());
			LOG.Debug("lastHeartBeat" + lastHeartBeat);
			LOG.Debug("lastHeartBeat.Add" + lastHeartBeat.AddMinutes(HEARTBEAT_INTERVAL).ToLongTimeString());
			LOG.Debug("<-IsExeededInterval");

			return !(DateTime.Now >= lastHeartBeat && DateTime.Now < lastHeartBeat.AddMinutes(HEARTBEAT_INTERVAL));
		}

		private void onConnectionActive()
		{
			LOG.Debug(String.Format("the connection is still active, last message was  {0}", lastHeartBeat));
		}

		private void onConnectionIdle()
		{
			LOG.Warn(String.Format("{0} canceling the long poll request because of inactivity", DateTime.Now.ToString("HH:mm:ss.fff")));
			foreach (IConnectionIdleListener listener in listeners)
			{
				listener.OnConnectionIdle();
			}
		}
	}
}