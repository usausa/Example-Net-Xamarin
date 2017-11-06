namespace FeliCaReader.FormsApp.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading;

    using FeliCaReader.FormsApp.Models;
    using FeliCaReader.FormsApp.Services;

    using Smart.Collections.Generic;
    using Smart.ComponentModel;
    using Smart.Forms.ViewModels;
    using Smart.Text;

    public class MainPageViewModel : ViewModelBase
    {
        public NotificationValue<string> Idm { get; } = new NotificationValue<string>();

        public NotificationValue<SuicaAccessData> Access { get; } = new NotificationValue<SuicaAccessData>();

        public ObservableCollection<SuicaLogData> Logs { get; } = new ObservableCollection<SuicaLogData>();

        public MainPageViewModel(IFeliCaService feliCaService)
        {
            Disposables.Add(feliCaService.Detected
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(OnDetected));
        }

        private void OnDetected(IFeliCaReader reader)
        {
            Idm.Value = string.Empty;
            Access.Value = null;
            Logs.Clear();

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

            Idm.Value = HexEncoder.ToHex(idm);
            Access.Value = Suica.ConvertToAccessData(block.BlockData);
            Logs.AddRange(blocks1.Concat(blocks2).Concat(blocks3)
                .Select(x => Suica.ConvertToLogData(x.BlockData))
                .Where(x => x != null)
                .ToArray());
        }
    }
}
