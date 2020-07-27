namespace NavigationSample.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using NavigationSample.Models.Entity;

    public class DataService
    {
        private readonly List<DataEntity> samples = new List<DataEntity>
        {
            new DataEntity { Id = 1, Name = "Sample-1" },
            new DataEntity { Id = 2, Name = "Sample-2" },
            new DataEntity { Id = 3, Name = "Sample-3" },
            new DataEntity { Id = 4, Name = "Sample-4" }
        };

        public IEnumerable<DataEntity> QuerySampleList()
        {
            return samples.Select(entity => new DataEntity { Id = entity.Id, Name = entity.Name });
        }

        public void InsertSample(string name)
        {
            samples.Add(new DataEntity { Id = samples.Count + 1, Name = name });
        }

        public void UpdateSample(DataEntity entity)
        {
            var current = samples.FirstOrDefault(x => x.Id == entity.Id);
            if (current is null)
            {
                return;
            }

            current.Name = entity.Name;
        }
    }
}
