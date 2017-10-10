namespace Inventory.Client.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Inventory.Client.Components;
    using Inventory.Client.Models.Entity;

    public class InspectionService
    {
        private readonly IFileService fileService;

        public InspectionService(IFileService fileService)
        {
            this.fileService = fileService;

            fileService.CreateDirectory(Definition.InspectionDirectory);
        }

        public bool IsInspectionExists(int storageNo)
        {
            var path = Path.Combine(Definition.InspectionDirectory, Definition.InspectionFileFormatter(storageNo));
            return fileService.IsFileExists(path);
        }

        public async Task DeleteInspectionLisAsynct(int storageNo)
        {
            var path = Path.Combine(Definition.InspectionDirectory, Definition.InspectionFileFormatter(storageNo));
            await fileService.DeleteFileAsync(path);
        }

        public async Task<InspectionStatusEntity[]> QueryInspectionStatusListAsync()
        {
            var list = new List<InspectionStatusEntity>();
            var buffer = new byte[InspectionStatusEntity.Size];

            foreach (var file in await fileService.GetFilesAsync(Definition.InspectionDirectory, "*"))
            {
                var path = Path.Combine(Definition.InspectionDirectory, file);
                using (var stream = await fileService.OpenReadAsync(path).ConfigureAwait(false))
                {
                    stream.Read(buffer, 0, buffer.Length);

                    var entity = new InspectionStatusEntity();
                    entity.FromBytes(buffer);

                    list.Add(entity);
                }
            }

            return list.ToArray();
        }

        public async Task UpdateAsync(InspectionStatusEntity status, IEnumerable<InspectionEntity> entities)
        {
            var path = Path.Combine(Definition.InspectionDirectory, Definition.InspectionFileFormatter(status.StorageNo));

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

        public async Task<StatusEntityPair<InspectionStatusEntity, InspectionEntity>> QueryInspectionAsync(int storageNo)
        {
            var path = Path.Combine(Definition.InspectionDirectory, Definition.InspectionFileFormatter(storageNo));

            if (!fileService.IsFileExists(path))
            {
                return new StatusEntityPair<InspectionStatusEntity, InspectionEntity>(
                    new InspectionStatusEntity { StorageNo = storageNo },
                    Enumerable.Empty<InspectionEntity>());
            }

            using (var stream = await fileService.OpenReadAsync(path).ConfigureAwait(false))
            {
                // Status
                var buffer = new byte[InspectionStatusEntity.Size];

                stream.Read(buffer, 0, buffer.Length);

                var status = new InspectionStatusEntity();
                status.FromBytes(buffer);

                // Entity
                var count = (int)((stream.Length - InspectionEntity.Size) / InspectionEntity.Size);
                var list = new List<InspectionEntity>(count);

                buffer = new byte[InspectionEntity.Size];
                for (var i = 0; i < count; i++)
                {
                    stream.Position = (InspectionEntity.Size * i) + InspectionStatusEntity.Size;
                    stream.Read(buffer, 0, buffer.Length);

                    var entity = new InspectionEntity { DetailNo = i + 1 };
                    entity.FromBytes(buffer);
                    list.Add(entity);
                }

                return new StatusEntityPair<InspectionStatusEntity, InspectionEntity>(status, list);
            }
        }
    }
}
