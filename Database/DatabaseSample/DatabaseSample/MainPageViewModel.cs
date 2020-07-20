namespace DatabaseSample
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using DatabaseSample.Models;
    using DatabaseSample.Services;

    using Smart.ComponentModel;
    using Smart.Forms.Input;
    using Smart.Forms.ViewModels;

    using XamarinFormsComponents.Dialogs;


    public class MainPageViewModel : ViewModelBase
    {
        public static MainPageViewModel DesignInstance { get; } = null; // For design

        private readonly IDialogs dialogs;

        private readonly DataService dataService;

        public NotificationValue<int> BulkDataCount { get; } = new NotificationValue<int>();

        public AsyncCommand InsertCommand { get; }
        public AsyncCommand UpdateCommand { get; }
        public AsyncCommand DeleteCommand { get; }
        public AsyncCommand QueryCommand { get; }

        public AsyncCommand BulkInsertCommand { get; }
        public AsyncCommand DeleteAllCommand { get; }
        public AsyncCommand QueryAllCommand { get; }

        public MainPageViewModel(
            IDialogs dialogs,
            DataService dataService)
        {
            this.dialogs = dialogs;
            this.dataService = dataService;

            InsertCommand = MakeAsyncCommand(Insert);
            UpdateCommand = MakeAsyncCommand(Update);
            DeleteCommand = MakeAsyncCommand(Delete);
            QueryCommand = MakeAsyncCommand(Query);

            BulkInsertCommand = MakeAsyncCommand(BulkInsert, () => BulkDataCount.Value == 0).Observe(BulkDataCount);
            DeleteAllCommand = MakeAsyncCommand(DeleteAll, () => BulkDataCount.Value > 0).Observe(BulkDataCount);
            QueryAllCommand = MakeAsyncCommand(QueryAll);
        }

        public async Task Initialize()
        {
            BulkDataCount.Value = await dataService.CountBulkDataAsync();
        }

        private async Task Insert()
        {
            var ret = await dataService.InsertDataAsync(new DataEntity { Id = 1L, Name = "Data-1", CreateAt = DateTime.Now });

            if (ret)
            {
                await dialogs.Information("Inserted");
            }
            else
            {
                await dialogs.Information("Key duplicate");
            }
        }

        private async Task Update()
        {
            var effect = await dataService.UpdateDataAsync(1L, "Updated");

            await dialogs.Information($"Effect={effect}");
        }

        private async Task Delete()
        {
            var effect = await dataService.DeleteDataAsync(1L);

            await dialogs.Information($"Effect={effect}");
        }

        private async Task Query()
        {
            var entity = await dataService.QueryDataAsync(1L);

            if (entity != null)
            {
                await dialogs.Information($"Name={entity.Name}\r\nDate={entity.CreateAt:yyyy/MM/dd HH:mm:ss}");
            }
            else
            {
                await dialogs.Information("Not found");
            }
        }

        private async Task BulkInsert()
        {
            var list = Enumerable.Range(1, 10000)
                .Select(x => new BulkDataEntity
                {
                    Key1 = $"{x / 1000:D2}",
                    Key2 = $"{x % 1000:D2}",
                    Key3 = "0",
                    Value1 = 1,
                    Value2 = 2,
                    Value3 = 3,
                    Value4 = 4,
                    Value5 = 5
                })
                .ToList();

            var watch = new Stopwatch();

            using (dialogs.Loading())
            {
                watch.Start();

                await Task.Run(() => dataService.InsertBulkDataEnumerable(list));

                watch.Stop();
            }

            BulkDataCount.Value = await dataService.CountBulkDataAsync();

            await dialogs.Information($"Inserted\r\nElapsed={watch.ElapsedMilliseconds}");
        }

        private async Task DeleteAll()
        {
            await dataService.DeleteAllBulkDataAsync();

            BulkDataCount.Value = await dataService.CountBulkDataAsync();
        }

        private async Task QueryAll()
        {
            var watch = new Stopwatch();

            using (dialogs.Loading())
            {
                watch.Start();

                await Task.Run(() => dataService.QueryAllBulkDataList());

                watch.Stop();
            }

            await dialogs.Information($"Query\r\nElapsed={watch.ElapsedMilliseconds}");
        }
    }
}
