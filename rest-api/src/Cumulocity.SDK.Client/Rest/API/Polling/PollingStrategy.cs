using Cumulocity.SDK.Client.Rest.API.Polling.Threads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.API.Polling
{
	public class PollingStrategy
	{

		public static readonly long? DEFAULT_POLL_CREDENTIALS_TIMEOUT = 3600L * 24;
		public static readonly long?[] DEFAULT_POLL_INTERVALS = new long?[] { 5L, 10L, 20L, 40L, 80L, 160L, 320L, 640L, 1280L, 2560L, 3600L };

		private readonly IList<long?> pollIntervals;
		private bool repeatLast = true;
		private readonly long? last;
		private int index;
		private readonly TimeUnit timeUnit;
		private readonly long? timeout;

		public PollingStrategy() : this(DEFAULT_POLL_CREDENTIALS_TIMEOUT, TimeUnit.SECONDS, DEFAULT_POLL_INTERVALS)
		{
		}

		public PollingStrategy(TimeUnit timeUnit, IList<long?> pollIntervals) : this(null, timeUnit, pollIntervals)
		{
		}

		public PollingStrategy(long? timeout, TimeUnit timeUnit, IList<long?> pollIntervals)
		{
			this.timeout = timeout;
			this.pollIntervals = pollIntervals;
			this.timeUnit = timeUnit;
			this.index = -1;
			this.last = pollIntervals.Count == 0 ? null : pollIntervals[pollIntervals.Count - 1];
		}

		public PollingStrategy(long? timeout, TimeUnit timeUnit, params long?[] pollIntervals) : this(timeout, timeUnit, pollIntervals.ToList())
		{
		}

		private long? forIndex(int index)
		{
			return index < pollIntervals.Count ? pollIntervals[index] : null;
		}

		public virtual bool Empty
		{
			get
			{
				return peakNext() == null;
			}
		}

		/// <summary>
		/// get next polling interval </summary>
		/// <returns> interval in milliseconds </returns>
		public virtual long? peakNext()
		{
			long? result = forIndex(index + 1);
			if (result == null && repeatLast)
			{
				result = last;
			}
			return asMiliseconds(result);
		}

		/// <summary>
		/// get and remove next polling interval </summary>
		/// <returns> interval </returns>
		public virtual long? popNext()
		{
			long? result = peakNext();
			if (result != null)
			{
				index++;
			}
			return result;
		}

		/// <returns> timeout </returns>
		public virtual long? Timeout
		{
			get
			{
				return asMiliseconds(timeout);
			}
		}


		public virtual bool RepeatLast
		{
			get
			{
				return repeatLast;
			}
			set
			{
				this.repeatLast = value;
			}
		}


		private long? asMiliseconds(long? result)
		{
			//return result == null ? null : MILLISECONDS.convert(result, timeUnit);
			if(timeUnit.Equals(TimeUnit.SECONDS))
				return result == null ? null : (long?)ConvertSecondsToMilliseconds(result);
			else
			{
				throw new Exception();
			}
		}
		//TODO: Add other methods
		public static double ConvertSecondsToMilliseconds(long? seconds)
		{
			return TimeSpan.FromSeconds(seconds ?? 0).TotalMilliseconds;
		}
	}


}
