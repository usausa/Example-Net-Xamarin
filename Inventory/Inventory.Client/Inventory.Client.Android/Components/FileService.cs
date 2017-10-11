namespace Inventory.Client.Droid.Components
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Inventory.Client.Components;

    public class FileService : IFileService
    {
        private string RootPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public Stream Open(string filename)
        {
            var path = Path.Combine(RootPath, filename);
            return File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        public async Task<Stream> OpenAsync(string filename, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            return Open(filename);
        }

        public Stream OpenRead(string filename)
        {
            var path = Path.Combine(RootPath, filename);
            return File.OpenRead(path);
        }

        public async Task<Stream> OpenReadAsync(string filename, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            return OpenRead(filename);
        }

        public void DeleteFile(string filename)
        {
            var path = Path.Combine(RootPath, filename);
            File.Delete(path);
        }

        public async Task DeleteFileAsync(string filename, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            DeleteFile(filename);
        }

        public void MoveFile(string source, string destination)
        {
            var sourcePath = Path.Combine(RootPath, source);
            var destinationPath = Path.Combine(RootPath, destination);
            File.Move(sourcePath, destinationPath);
        }

        public async Task MoveFileAsync(string source, string destination, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            MoveFile(source, destination);
        }

        public void CopyFile(string source, string destination, bool overwrite = false)
        {
            var sourcePath = Path.Combine(RootPath, source);
            var destinationPath = Path.Combine(RootPath, destination);
            File.Copy(sourcePath, destinationPath, overwrite);
        }

        public async Task CopyFileAsync(string source, string destination, bool overwrite, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            CopyFile(source, destination, overwrite);
        }

        public bool IsFileExists(string filename)
        {
            var path = Path.Combine(RootPath, filename);
            return File.Exists(path);
        }

        public async Task<bool> IsFileExistsAsync(string filename, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            return IsFileExists(filename);
        }

        public void CreateDirectory(string directory)
        {
            var path = Path.Combine(RootPath, directory);
            Directory.CreateDirectory(path);
        }

        public async Task CreateDirectoryAsync(string directory, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            CreateDirectory(directory);
        }

        public void DeleteDirectory(string directory, bool recursive)
        {
            var path = Path.Combine(RootPath, directory);
            Directory.Delete(path, recursive);
        }

        public async Task DeleteDirectoryAsync(string directory, bool recursive, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            DeleteDirectory(directory, recursive);
        }

        public void MoveDirectory(string source, string destination)
        {
            var sourcePath = Path.Combine(RootPath, source);
            var destinationPath = Path.Combine(RootPath, destination);
            Directory.Move(sourcePath, destinationPath);
        }

        public async Task MoveDirectoryAsync(string source, string destination, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            MoveDirectory(source, destination);
        }

        public bool IsDirectoryExists(string directory)
        {
            var path = Path.Combine(RootPath, directory);
            return Directory.Exists(path);
        }

        public async Task<bool> IsDirectoryExistsAsync(string directory, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);

            return IsDirectoryExists(directory);
        }

        public string[] GetFiles(string directory, string pattern)
        {
            var path = Path.Combine(RootPath, directory);
            return Directory.GetFiles(path, pattern)
                .Select(x => new FileInfo(x).Name)
                .ToArray();
        }

        public async Task<string[]> GetFilesAsync(string directory, string pattern, CancellationToken token)
        {
            await AwaitExtensions.SwitchOffMainThreadAsync(token);
            return GetFiles(directory, pattern);
        }
    }
}