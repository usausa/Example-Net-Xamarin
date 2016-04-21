namespace Example.FormsApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public class DataService
    {
        private List<DataEntity> entities = Enumerable.Range(1, 5)
            .Select(_ => new DataEntity { Id = _, Name = String.Format(CultureInfo.InvariantCulture, "Data-{0}", _) })
            .ToList();

        public IEnumerable<DataEntity> QueryEntityList()
        {
            return entities;
        }
    }
}
