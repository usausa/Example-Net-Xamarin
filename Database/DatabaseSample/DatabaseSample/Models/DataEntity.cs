namespace DatabaseSample.Models
{
    using System;

    using Smart.Data.Mapper.Attributes;

    public class DataEntity
    {
        [PrimaryKey]
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
