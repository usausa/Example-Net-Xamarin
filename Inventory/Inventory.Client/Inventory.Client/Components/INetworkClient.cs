namespace Inventory.Client.Components
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface INetworkClient
    {
        Task<NetworkResult<T>> Get<T>(string path, TimeSpan? timeout = null, CancellationToken token = default(CancellationToken));

        Task<NetworkResult> Post<T>(string path, T request, TimeSpan? timeout = null, CancellationToken token = default(CancellationToken));

        Task<NetworkResult> Download(string path, Stream stream, TimeSpan? timeout = null, CancellationToken token = default(CancellationToken));
    }
}
