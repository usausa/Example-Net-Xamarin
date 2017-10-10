namespace Inventory.Client.Components
{
    using System;
    using System.Threading.Tasks;

    public static class LoadingServiceExtensions
    {
        public static void WithExecute(
            this ILoadingService loadingService,
            string message,
            Action execute)
        {
            try
            {
                loadingService.Show(message);

                execute();
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static TResult WithExecute<TResult>(
            this ILoadingService loadingService,
            string message,
            Func<TResult> execute)
        {
            try
            {
                loadingService.Show(message);

                return execute();
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static async Task WithExecuteAsync(
            this ILoadingService loadingService,
            string message,
            Func<Task> execute)
        {
            try
            {
                loadingService.Show(message);

                await execute();
            }
            finally
            {
                loadingService.Hide();
            }
        }

        public static async Task<TResult> WithExecuteAsync<TResult>(
            this ILoadingService loadingService,
            string message,
            Func<Task<TResult>> execute)
        {
            try
            {
                loadingService.Show(message);

                return await execute();
            }
            finally
            {
                loadingService.Hide();
            }
        }
    }
}
