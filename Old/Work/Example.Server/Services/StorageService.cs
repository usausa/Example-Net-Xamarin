namespace Example.Server.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Dapper;

    using Example.Server.Infrastructure.Data;
    using Example.Server.Models.Entity;
    using Example.Server.Models.View;

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
                    "SELECT * FROM storage_ciew WHERE storage_no = @StorageNo",
                    new { StorageNo = storageNo }));
        }

        public async Task<IEnumerable<StorageView>> QueryStorageViewList()
        {
            return await ConnectionFactory.UsingAsync(con =>
                con.QueryAsync<StorageView>("SELECT * FROM storage_view ORDER BY storage_no"));
        }

        public async Task<bool> UpdateStorage(bool renew, int storageNo, int userId, IEnumerable<StorageDetailEntity> details)
        {
            return await ConnectionFactory.UsingTxAsync(async (con, tx) =>
            {
                if (renew)
                {
                    await con.ExecuteAsync(
                        "DELETE FROM storage WHERE storage_no = @storage_no",
                        new { StorageNo = storageNo },
                        tx);
                    await con.ExecuteAsync(
                        "INSERT INTO storage (storage_no, entry_user_id, entry_at) VALUES (@StorageNo, @EntryUserId, CURRENT_TIMESTAMP)",
                        new { StorageNo = storageNo, EntryUserId = userId },
                        tx);
                }
                else
                {
                    var effect = await con.ExecuteAsync(
                        "UPDATE storage SET inspection_user_id = @InspectionUserId, inspection_at = CURRENT_TIMESTAMP WHERE storage_no = @StorageNo",
                        new { StorageNo = storageNo, InspectionUserId = userId },
                        tx);
                    if (effect == 0)
                    {
                        return false;
                    }

                    await con.ExecuteAsync(
                        "DELETE FROM storage_detail WHERE storage_no = @StorageNo",
                        new { StorageNo = storageNo },
                        tx);
                }

                foreach (var detail in details)
                {
                    await con.ExecuteAsync(
                        "INSERT INTO storage_detail (storage_no, detail_no, item_code, item_name, sales_price, amount) " +
                        "VALUES (@StorageNo, @DetailNo, @ItemCode, @ItemName, @SalesPrice, @Amount)",
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
                    "DELETE FROM storage WHERE storage_no = @StorageNo",
                    new { StorageNo = storageNo });
            });
        }

        public async Task DeleteStorageAll()
        {
            await ConnectionFactory.UsingAsync(async con =>
            {
                await con.ExecuteAsync("DELETE FROM storage");
            });
        }

        public async Task<IEnumerable<StorageDetailEntity>> QueryStorageDetailList(int storageNo)
        {
            return await ConnectionFactory.UsingAsync(con =>
                con.QueryAsync<StorageDetailEntity>(
                    "SELECT * FROM storage_detail WHERE storage_no = @StorageNo ORDER BY detail_no",
                    new { StorageNo = storageNo }));
        }
    }
}
