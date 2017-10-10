namespace Inventory.Client.Models.Entity
{
    using System.Collections.Generic;

    public class StatusEntityPair<TStatus, TEntity>
    {
        public TStatus Status { get; }

        public IEnumerable<TEntity> Entities { get; }

        public StatusEntityPair(TStatus status, IEnumerable<TEntity> entities)
        {
            Status = status;
            Entities = entities;
        }
    }
}
