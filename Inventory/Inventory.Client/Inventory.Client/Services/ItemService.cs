namespace Inventory.Client.Services
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Helpers;
    using Inventory.Client.Models.Entity;

    public class ItemService
    {
        private static readonly Encoding SjisEncoding = Encoding.GetEncoding("Shift_JIS");

        private readonly IFileService fileService;

        public ItemService(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public bool IsItemExists()
        {
            return fileService.IsFileExists(Definition.ItemFile);
        }

        public async Task DeleteItem()
        {
            await fileService.DeleteFileAsync(Definition.ItemFile);
        }

        public async Task<Stream> OpenItemAsync()
        {
            await fileService.DeleteFileAsync(Definition.ItemFile);

            return await fileService.OpenAsync(Definition.ItemFile);
        }

        public async Task<ItemEntity> FindItemAsync(string code)
        {
            using (var stream = await fileService.OpenReadAsync(Definition.ItemFile).ConfigureAwait(false))
            {
                var count = (int)(stream.Length / ItemEntity.Size);

                var pos = SearchHelper.Find(count, MakeFinder(stream, SjisEncoding.GetBytes(code)));
                if (pos < 0)
                {
                    return null;
                }

                var buffer = new byte[ItemEntity.Size];

                stream.Position = pos * ItemEntity.Size;
                stream.Read(buffer, 0, buffer.Length);

                return new ItemEntity(buffer);
            }
        }

        private Func<int, int> MakeFinder(Stream stream, byte[] code)
        {
            var buffer = new byte[ItemEntity.ItemCodeLength];

            return index =>
            {
                stream.Position = index * ItemEntity.Size;
                stream.Read(buffer, 0, buffer.Length);

                return ByteHelper.CompareBytes(buffer, code);
            };
        }
    }
}
