namespace FeliCaReader.FormsApp.Pages
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading;

    using FeliCaReader.FormsApp.Models;
    using FeliCaReader.FormsApp.Services;

    using Smart.Forms.ViewModels;

    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(IFeliCaService feliCaService)
        {
            Disposables.Add(feliCaService.Detected
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(OnDetected));
        }

        private void OnDetected(IFeliCaReader reader)
        {
            var idm = reader.ExecutePolling(0x0003);
            if (idm.Length == 0)
            {
                return;
            }

            var block = new ReadBlock { BlockNo = 0 };
            if (!reader.ExecuteReadWoe(idm, 0x008B, block))
            {
                return;
            }

            var blocks1 = Enumerable.Range(0, 8).Select(x => new ReadBlock { BlockNo = (byte)x }).ToArray();
            var blocks2 = Enumerable.Range(8, 8).Select(x => new ReadBlock { BlockNo = (byte)x }).ToArray();
            var blocks3 = Enumerable.Range(16, 4).Select(x => new ReadBlock { BlockNo = (byte)x }).ToArray();
            if (!reader.ExecuteReadWoe(idm, 0x090F, blocks1) ||
                !reader.ExecuteReadWoe(idm, 0x090F, blocks2) ||
                !reader.ExecuteReadWoe(idm, 0x090F, blocks3))
            {
                return;
            }

            // Debug
            var access = Suica.ConvertToAccessData(block.BlockData);
            var logs = blocks1.Concat(blocks2).Concat(blocks3)
                .Select(x => Suica.ConvertToLogData(x.BlockData))
                .Where(x => x != null)
                .ToArray();

            System.Diagnostics.Debug.WriteLine($"[残額] {access.Balance}");
            System.Diagnostics.Debug.WriteLine("[履歴]");
            foreach (var log in logs)
            {
                var withCash = log.WithCash ? " 現金併用" : string.Empty;
                if (Suica.IsProcessOfSales(log.ProcessType))
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"機器=[{Suica.ConvertTerminalString(log.TerminalType)}], " +
                        $"利用=[{Suica.ConvertProcessString(log.ProcessType)}{withCash}], " +
                        $"取引日時=[{log.DateTime:yyyy/MM/dd HH:mm}], " +
                        $"残額=[{log.Balance}], " +
                        $"取引通番=[{log.TransactionId}]");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"機器=[{Suica.ConvertTerminalString(log.TerminalType)}], " +
                        $"利用=[{Suica.ConvertProcessString(log.ProcessType)}{withCash}], " +
                        $"取引日=[{log.DateTime:yyyy/MM/dd}], " +
                        $"残額=[{log.Balance}], " +
                        $"取引通番=[{log.TransactionId}]");
                }
            }
        }
    }
}
