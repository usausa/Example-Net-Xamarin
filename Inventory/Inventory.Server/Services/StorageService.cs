namespace Inventory.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dapper;

    using Inventory.Server.Models.Entity;
    using Inventory.Server.Models.View;

    using Smart.Data;

    public class StorageService
    {
        private IConnectionFactory ConnectionFactory { get; }

        public StorageService(IConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public async Task<StorageView> QueryStorageView(int storageNo)
        {
            return await ConnectionFactory.UsingAsync(con =>
                con.QueryFirstOrDefaultAsync<StorageView>(
                    "SELECT * FROM StorageView WHERE StorageNo = @StorageNo",
                    new { StorageNo = storageNo }));
        }

        public async Task<IEnumerable<StorageView>> QueryStorageViewList()
        {
            return await ConnectionFactory.UsingAsync(con =>
                con.QueryAsync<StorageView>("SELECT * FROM StorageView ORDER BY StorageNo"));
        }

        public async Task<bool> UpdateStorage(bool renew, int storageNo, int userId, IEnumerable<StorageDetailEntity> details)
        {
            return await ConnectionFactory.UsingTxAsync(async (con, tx) =>
            {
                if (renew)
                {
                    await con.ExecuteAsync(
                        "DELETE FROM Storage WHERE StorageNo = @StorageNo",
                        new { StorageNo = storageNo },
                        tx);
                    await con.ExecuteAsync(
                        "INSERT INTO Storage (StorageNo, EntryUserId, EntryAt) VALUES (@StorageNo, @EntryUserId, SYSDATETIME())",
                        new { StorageNo = storageNo, EntryUserId = userId },
                        tx);
                }
                else
                {
                    var effect = await con.ExecuteAsync(
                        "UPDATE Storage SET InspectionUserId = @InspectionUserId, InspectionAt = SYSDATETIME() WHERE StorageNo = @StorageNo",
                        new { StorageNo = storageNo, InspectionUserId = userId },
                        tx);
                    if (effect == 0)
                    {
                        return false;
                    }

                    await con.ExecuteAsync(
                        "DELETE FROM StorageDetail WHERE StorageNo = @StorageNo",
                        new { StorageNo = storageNo },
                        tx);
                }

                foreach (var detail in details)
                {
                    await con.ExecuteAsync(
                        "INSERT INTO StorageDetail (StorageNo, DetailNo, ItemCode, ItemName, SalesPrice, Qty) " +
                        "VALUES (@StorageNo, @DetailNo, @ItemCode, @ItemName, @SalesPrice, @Qty)",
                        detail,
                        tx);
                }

                tx.Commit();

                return true;
            });
        }

        public async Task DeleteStorage(int storageNo)
        {
            await ConnectionFactory.UsingAsync(async con =>
            {
                await con.ExecuteAsync(
                    "DELETE FROM Storage WHERE StorageNo = @StorageNo",
                    new { StorageNo = storageNo });
            });
        }

        public async Task DeleteStorageAll()
        {
            await ConnectionFactory.UsingAsync(async con =>
            {
                await con.ExecuteAsync("DELETE FROM Storage");
            });
        }

        public async Task<IEnumerable<StorageDetailEntity>> QueryStorageDetailList(int storageNo)
        {
            return await ConnectionFactory.UsingAsync(con =>
                con.QueryAsync<StorageDetailEntity>(
                    "SELECT * FROM StorageDetail WHERE StorageNo = @StorageNo ORDER BY DetailNo",
                    new { StorageNo = storageNo }));
        }
    }
}
