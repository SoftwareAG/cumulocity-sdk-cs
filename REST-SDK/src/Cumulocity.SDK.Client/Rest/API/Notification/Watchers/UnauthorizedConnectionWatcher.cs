﻿using Cumulocity.SDK.Client.Rest.API.Notification.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Cumulocity.SDK.Client.Logging;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Watchers
{
	public class UnauthorizedConnectionWatcher
	{
		private static readonly ILog LOG = LogProvider.For<UnauthorizedConnectionWatcher>();
		private const int RETRY_COUNT_AFTER_UNAUTHORIZED = 5;

		private readonly IList<IConnectionListener> listeners = new List<IConnectionListener>();

		private int retryCounter = RETRY_COUNT_AFTER_UNAUTHORIZED;

		public virtual void unauthorizedAccess()
		{
			retryCounter--;
			if (shouldRetry())
			{
				LOG.Info("bayeux client received 401, still trying '{}' times before stopping reconnection", retryCounter);
				Debug.WriteLine(String.Format("bayeux client received 401, still trying '{0}' times before stopping reconnection", retryCounter));
			}
			else
			{
				LOG.Warn("bayeux client received 401 too many times -> do no longer reconnect");
				Debug.WriteLine("bayeux client received 401 too many times -> do no longer reconnect");
				foreach (IConnectionListener listener in listeners)
				{
					//listener.OnDisconnection(SC_UNAUTHORIZED);
					listener.OnDisconnection(401);
				}
			}
		}

		public virtual bool shouldRetry()
		{
			return retryCounter > 0;
		}

		public virtual void resetCounter()
		{
			Debug.WriteLine("Resetting unauthorized retry counter");
			retryCounter = RETRY_COUNT_AFTER_UNAUTHORIZED;
		}

		//WARNING: 'final' parameters are not available in .NET:
		//ORIGINAL LINE: public void addListener(final ConnectionListener listener)
		public virtual void AddListener(IConnectionListener listener)
		{
			listeners.Add(listener);
		}
	}
}