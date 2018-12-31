using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Notification.Watchers
{
	public abstract class EndlessTask
	{
		protected readonly int ExecutionLoopDelayMs = 0;
		protected Task TheNeverEndingTask;
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();

		protected EndlessTask(int executionLoopDelayMin)
		{
			ExecutionLoopDelayMs = executionLoopDelayMin * 1000;

			TheNeverEndingTask = new Task(
				async () =>
				{
					await Task.Delay(12 * 60 * 1000);

					do
					{
						ExecutionCore(_cts.Token);
					} while (!_cts.Token.WaitHandle.WaitOne(ExecutionLoopDelayMs));

					if (_cts.IsCancellationRequested)
						return;
				},
				_cts.Token,
				TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning);

			// Do not forget to observe faulted tasks - for NeverEndingTask faults are probably never desirable
			TheNeverEndingTask.ContinueWith(x =>
			{
				Trace.TraceError(x.Exception.InnerException.Message);
				// Log/Fire Events etc.
			}, TaskContinuationOptions.OnlyOnFaulted);
		}

		public void StartRun(int initialDelayMin)
		{
			// Should throw if you try to start twice...
			try
			{
				TheNeverEndingTask.Start();
			}
			catch (OperationCanceledException ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public void StopRun()
		{
			_cts.Cancel();
			TheNeverEndingTask.Wait();
		}

		protected abstract void ExecutionCore(CancellationToken cancellationToken);
	}
}