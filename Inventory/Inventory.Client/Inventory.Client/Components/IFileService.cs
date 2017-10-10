namespace Inventory.Client.Components
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IFileService
    {
        Stream Open(string filename);

        Task<Stream> OpenAsync(string filename, CancellationToken token = default);

        Stream OpenRead(string filename);

        Task<Stream> OpenReadAsync(string filename, CancellationToken token = default);

        void DeleteFile(string filename);

        Task DeleteFileAsync(string filename, CancellationToken token = default);

        void MoveFile(string source, string destination);

        Task MoveFileAsync(string source, string destination, CancellationToken token = default);

        void CopyFile(string source, string destination, bool overwrite = false);

        Task CopyFileAsync(string source, string destination, bool overwrite = false, CancellationToken token = default);

        bool IsFileExists(string filename);

        Task<bool> IsFileExistsAsync(string filename, CancellationToken token = default);

        void CreateDirectory(string directory);

        Task CreateDirectoryAsync(string directory, CancellationToken token = default);

        void DeleteDirectory(string directory, bool recursive = false);

        Task DeleteDirectoryAsync(string directory, bool recursive = false, CancellationToken token = default);

        void MoveDirectory(string source, string destination);

        Task MoveDirectoryAsync(string source, string destination, CancellationToken token = default);

        bool IsDirectoryExists(string directory);

        Task<bool> IsDirectoryExistsAsync(string directory, CancellationToken token = default);

        string[] GetFiles(string directory, string pattern);

        Task<string[]> GetFilesAsync(string directory, string pattern, CancellationToken token = default);
    }
}
