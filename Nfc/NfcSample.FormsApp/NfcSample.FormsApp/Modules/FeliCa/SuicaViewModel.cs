namespace NfcSample.FormsApp.Modules.FeliCa;

using System.Collections.ObjectModel;
using System.Reactive.Linq;

using NfcSample.FormsApp.Components.Nfc;
using NfcSample.FormsApp.Models.FeliCa;

using Smart.Collections.Generic;
using Smart.ComponentModel;
using Smart.Navigation;
using Smart.Text;

public class SuicaViewModel : AppViewModelBase
{
    private readonly INfcReader nfcReader;

    public NotificationValue<string> Idm { get; } = new();

    public NotificationValue<SuicaAccessData?> Access { get; } = new();

    public ObservableCollection<SuicaLogData> Logs { get; } = new();

    public SuicaViewModel(
        ApplicationState applicationState,
        INfcReader nfcReader)
        : base(applicationState)
    {
        this.nfcReader = nfcReader;

        Disposables.Add(nfcReader.TechDiscovered
            .ObserveOn(SynchronizationContext.Current)
            .Subscribe(OnTechDiscovered));
    }

    public override void OnNavigatingFrom(INavigationContext context)
    {
        nfcReader.NfcType = NfcType.TypeF;
        nfcReader.Enable = false;
    }

    public override void OnNavigatingTo(INavigationContext context)
    {
        nfcReader.Enable = true;
    }

    protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.Menu);

    protected override Task OnNotifyFunction1() => OnNotifyBackAsync();

    private void OnTechDiscovered(INfc nfc)
    {
        Idm.Value = string.Empty;
        Access.Value = null;
        Logs.Clear();

        var idm = nfc.ExecutePolling(0x0003);
        if (idm.Length == 0)
        {
            return;
        }

        var block = new ReadBlock { BlockNo = 0 };
        if (!nfc.ExecuteReadWoe(idm, 0x008B, block))
        {
            return;
        }

        var blocks1 = Enumerable.Range(0, 8).Select(x => new ReadBlock { BlockNo = (byte)x }).ToArray();
        var blocks2 = Enumerable.Range(8, 8).Select(x => new ReadBlock { BlockNo = (byte)x }).ToArray();
        var blocks3 = Enumerable.Range(16, 4).Select(x => new ReadBlock { BlockNo = (byte)x }).ToArray();
        if (!nfc.ExecuteReadWoe(idm, 0x090F, blocks1) ||
            !nfc.ExecuteReadWoe(idm, 0x090F, blocks2) ||
            !nfc.ExecuteReadWoe(idm, 0x090F, blocks3))
        {
            return;
        }

        Idm.Value = HexEncoder.Encode(idm);
        Access.Value = Suica.ConvertToAccessData(block.BlockData);
        Logs.AddRange(blocks1.Concat(blocks2).Concat(blocks3)
            .Select(x => Suica.ConvertToLogData(x.BlockData))
            .WhereNotNull()
            .ToArray());
    }
}
