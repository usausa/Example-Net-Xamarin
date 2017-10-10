namespace Inventory.Client.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Models.Entity;

    public class EntryService
    {
        private readonly IFileService fileService;

        public EntryService(IFileService fileService)
        {
            this.fileService = fileService;

            fileService.CreateDirectory(Definition.EntryDirectory);
        }

        public async Task DeleteEntryLisAsynct(int storageNo)
        {
            var path = Path.Combine(Definition.EntryDirectory, Definition.EntryFileFormatter(storageNo));
            await fileService.DeleteFileAsync(path);
        }

        public async Task<EntryStatusEntity[]> QueryEntryStatusListAsync()
        {
            var list = new List<EntryStatusEntity>();
            var buffer = new byte[EntryStatusEntity.Size];

            foreach (var file in await fileService.GetFilesAsync(Definition.EntryDirectory, "*"))
            {
                var path = Path.Combine(Definition.EntryDirectory, file);
                using (var stream = await fileService.OpenReadAsync(path).ConfigureAwait(false))
                {
                    stream.Read(buffer, 0, buffer.Length);

                    var entity = new EntryStatusEntity();
                    entity.FromBytes(buffer);

                    list.Add(entity);
                }
            }

            return list.ToArray();
        }

        public async Task UpdateAsync(EntryStatusEntity status, IEnumerable<EntryEntity> entities)
        {
            var path = Path.Combine(Definition.EntryDirectory, Definition.EntryFileFormatter(status.StorageNo));

            await fileService.DeleteFileAsync(path);

            using (var stream = await fileService.OpenAsync(path).ConfigureAwait(false))
            {
                var buffer = status.ToBytes();

                stream.Write(buffer, 0, buffer.Length);

                foreach (var entity in entities)
                {
                    buffer = entity.ToBytes();

                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public async Task<StatusEntityPair<EntryStatusEntity, EntryEntity>> QueryEntryAsync(int storageNo)
        {
            var path = Path.Combine(Definition.EntryDirectory, Definition.EntryFileFormatter(storageNo));

            if (!fileService.IsFileExists(path))
            {
                return new StatusEntityPair<EntryStatusEntity, EntryEntity>(
                    new EntryStatusEntity { StorageNo = storageNo },
                    Enumerable.Empty<EntryEntity>());
            }

            using (var stream = await fileService.OpenReadAsync(path).ConfigureAwait(false))
            {
                // Status
                var buffer = new byte[EntryStatusEntity.Size];

                stream.Read(buffer, 0, buffer.Length);

                var status = new EntryStatusEntity();
                status.FromBytes(buffer);

                // Entity
                var count = (int)((stream.Length - EntryStatusEntity.Size) / EntryEntity.Size);
                var list = new List<EntryEntity>(count);

                buffer = new byte[EntryEntity.Size];
                for (var i = 0; i < count; i++)
                {
                    stream.Position = (EntryEntity.Size * i) + EntryStatusEntity.Size;
                    stream.Read(buffer, 0, buffer.Length);

                    var entity = new EntryEntity { DetailNo = i + 1 };
                    entity.FromBytes(buffer);
                    list.Add(entity);
                }

                return new StatusEntityPair<EntryStatusEntity, EntryEntity>(status, list);
            }
        }
    }
}
