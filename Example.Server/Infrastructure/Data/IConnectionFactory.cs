namespace Example.Server.Infrastructure.Data
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();
    }

    /// <summary>
    ///
    /// </summary>
    public static class ConnectionFactoryExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="action"></param>
        public static void Using(this IConnectionFactory factory, Action<IDbConnection> action)
        {
            using (var con = factory.CreateConnection())
            {
                action(con);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T Using<T>(this IConnectionFactory factory, Func<IDbConnection, T> func)
        {
            using (var con = factory.CreateConnection())
            {
                return func(con);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task UsingAsync(this IConnectionFactory factory, Func<IDbConnection, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                await func(con);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<T> UsingAsync<T>(this IConnectionFactory factory, Func<IDbConnection, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                return await func(con);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="action"></param>
        public static void UsingTx(this IConnectionFactory factory, Action<IDbConnection, IDbTransaction> action)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    action(con, tx);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T UsingTx<T>(this IConnectionFactory factory, Func<IDbConnection, IDbTransaction, T> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    return func(con, tx);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task UsingTxAsync(this IConnectionFactory factory, Func<IDbConnection, IDbTransaction, Task> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    await func(con, tx);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<T> UsingTxAsync<T>(this IConnectionFactory factory, Func<IDbConnection, IDbTransaction, Task<T>> func)
        {
            using (var con = factory.CreateConnection())
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    return await func(con, tx);
                }
            }
        }
    }
}
