namespace DatabaseSample.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using DatabaseSample.Helpers;
    using DatabaseSample.Models;

    using Microsoft.Data.Sqlite;

    using Smart.Data;
    using Smart.Data.Mapper;
    //using Smart.Data.Mapper.Builders;

    public class DataServiceOptions
    {
        public string Path { get; set; }
    }

    public class DataService
    {
        private readonly DataServiceOptions options;

        private readonly DelegateDbProvider provider;

        public DataService(DataServiceOptions options)
        {
            this.options = options;

            var connectionString = $"Data Source={options.Path}";
            provider = new DelegateDbProvider(() => new SqliteConnection(connectionString));
        }

        public async ValueTask RebuildAsync()
        {
            if (File.Exists(options.Path))
            {
                File.Delete(options.Path);
            }

            await provider.UsingAsync(async con =>
            {
                await con.ExecuteAsync("PRAGMA AUTO_VACUUM=1");
                await con.ExecuteAsync(SqlHelper.MakeCreate<DataEntity>());
                await con.ExecuteAsync(SqlHelper.MakeCreate<BulkDataEntity>());
            });
        }

        // CRUD

        public async ValueTask<bool> InsertDataAsync(DataEntity entity)
        {
            return await provider.UsingAsync(async con =>
            {
                try
                {
                    await con.ExecuteAsync(
                        //SqlInsert<DataEntity>.Values(),
                        "INSERT INTO Data (Id, Name, CreateAt) VALUES (@Id, @Name, @CreateAt)",
                        entity);

                    return true;
                }
                catch (SqliteException e)
                {
                    if (e.SqliteErrorCode == SQLitePCL.raw.SQLITE_CONSTRAINT)
                    {
                        return false;
                    }
                    throw;
                }
            });
        }

        public async ValueTask<int> UpdateDataAsync(long id, string name)
        {
            return await provider.UsingAsync(con =>
                con.ExecuteAsync(
                    //SqlUpdate<DataEntity>.Set("Name = @Name", "Id = @Id"),
                    "UPDATE Data SET Name = @Name WHERE Id = @Id",
                    new { Id = id, Name = name }));
        }

        public async ValueTask<int> DeleteDataAsync(long id)
        {
            return await provider.UsingAsync(con =>
                con.ExecuteAsync(
                    //SqlDelete<DataEntity>.ByKey(),
                    "DELETE FROM Data WHERE Id = @Id",
                    new { Id = id }));
        }

        public async ValueTask<DataEntity> QueryDataAsync(long id)
        {
            return await provider.UsingAsync(con =>
                con.QueryFirstOrDefaultAsync<DataEntity>(
                    //SqlSelect<DataEntity>.ByKey(),
                    "SELECT * FROM Data WHERE Id = @Id",
                    new { Id = id }));
        }

        // Bulk

        public async ValueTask<int> CountBulkDataAsync()
        {
            return await provider.UsingAsync(con =>
                con.ExecuteScalarAsync<int>(
                    //SqlCount<BulkDataEntity>.All()));
                    "SELECT COUNT(*) FROM BulkData"));
        }

        public void InsertBulkDataEnumerable(IEnumerable<BulkDataEntity> source)
        {
            provider.UsingTx((con, tx) =>
            {
                foreach (var entity in source)
                {
                    con.Execute(
                        //SqlInsert<BulkDataEntity>.Values(),
                        "INSERT INTO BulkData (Key1, Key2, Key3, Value1, Value2, Value3, Value4, Value5) VALUES (@Key1, @Key2, @Key3, @Value1, @Value2, @Value3, @Value4, @Value5)",
                        entity,
                        tx);
                }

                tx.Commit();
            });
        }

        public async ValueTask DeleteAllBulkDataAsync()
        {
            await provider.UsingAsync(con => con.ExecuteAsync("DELETE FROM BulkData"));
        }

        public List<BulkDataEntity> QueryAllBulkDataList()
        {
            return provider.Using(con =>
                con.QueryList<BulkDataEntity>(
                    //SqlSelect<BulkDataEntity>.All()));
                    "SELECT * FROM BulkData ORDER BY Key1, Key2, Key3"));
        }
    }
}
