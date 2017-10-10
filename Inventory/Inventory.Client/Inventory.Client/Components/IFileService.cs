namespace Inventory.Client.Components
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IFileService
    {
        Stream Open(string filename);

        Task<Stream> OpenAsync(string filename, CancellationToken token = default(CancellationToken));

        Stream OpenRead(string filename);

        Task<Stream> OpenReadAsync(string filename, CancellationToken token = default(CancellationToken));

        void DeleteFile(string filename);

        Task DeleteFileAsync(string filename, CancellationToken token = default(CancellationToken));

        void MoveFile(string source, string destination);

        Task MoveFileAsync(string source, string destination, CancellationToken token = default(CancellationToken));

        void CopyFile(string source, string destination, bool overwrite = false);

        Task CopyFileAsync(string source, string destination, bool overwrite = false, CancellationToken token = default(CancellationToken));

        bool IsFileExists(string filename);

        Task<bool> IsFileExistsAsync(string filename, CancellationToken token = default(CancellationToken));

        void CreateDirectory(string directory);

        Task CreateDirectoryAsync(string directory, CancellationToken token = default(CancellationToken));

        void DeleteDirectory(string directory, bool recursive = false);

        Task DeleteDirectoryAsync(string directory, bool recursive = false, CancellationToken token = default(CancellationToken));

        void MoveDirectory(string source, string destination);

        Task MoveDirectoryAsync(string source, string destination, CancellationToken token = default(CancellationToken));

        bool IsDirectoryExists(string directory);

        Task<bool> IsDirectoryExistsAsync(string directory, CancellationToken token = default(CancellationToken));

        string[] GetFiles(string directory, string pattern);

        Task<string[]> GetFilesAsync(string directory, string pattern, CancellationToken token = default(CancellationToken));
    }
}
