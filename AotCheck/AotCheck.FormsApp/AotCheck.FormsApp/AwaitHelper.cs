namespace AotCheck.FormsApp
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public static class AwaitHelper
    {
        internal static TaskSchedulerAwaiter SwitchOffMainThreadAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return new TaskSchedulerAwaiter(
                SynchronizationContext.Current != null ? TaskScheduler.Default : null,
                cancellationToken);
        }

        internal struct TaskSchedulerAwaiter : INotifyCompletion
        {
            private readonly TaskScheduler taskScheduler;

            private CancellationToken cancellationToken;

            public bool IsCompleted => taskScheduler == null;

            internal TaskSchedulerAwaiter(TaskScheduler taskScheduler, CancellationToken cancellationToken)
            {
                this.taskScheduler = taskScheduler;
                this.cancellationToken = cancellationToken;
            }

            internal TaskSchedulerAwaiter GetAwaiter()
            {
                return this;
            }

            public void OnCompleted(Action continuation)
            {
                if (taskScheduler == null)
                {
                    throw new InvalidOperationException("IsCompleted is true, so this is unexpected.");
                }

                Task.Factory.StartNew(
                    continuation,
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    taskScheduler);
            }

            public void GetResult()
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
