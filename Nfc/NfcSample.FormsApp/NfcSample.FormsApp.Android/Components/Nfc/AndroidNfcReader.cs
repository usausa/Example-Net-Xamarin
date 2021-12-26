namespace NfcSample.FormsApp.Droid.Components.Nfc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

using Android.App;
using Android.Content;
using Android.Nfc;
using Android.Nfc.Tech;

using NfcSample.FormsApp.Components.Nfc;

public sealed class AndroidNfcReader : INfcReader
{
    private readonly Activity activity;

    private readonly NfcAdapter nfcAdapter;

    private readonly Subject<INfc> subject = new();

    private bool enabled;

    public IObservable<INfc> TechDiscovered => subject;

    public NfcType NfcType { get; set; }

    public bool Enable
    {
        get => enabled;
        set
        {
            if (enabled != value)
            {
                enabled = value;
                if (enabled)
                {
                    Start();
                }
                else
                {
                    Stop();
                }
            }
        }
    }

    public AndroidNfcReader(Activity activity)
    {
        this.activity = activity;
        var nfcManager = (NfcManager)Application.Context.GetSystemService(Context.NfcService)!;
        nfcAdapter = nfcManager.DefaultAdapter!;
    }

    public void Pause()
    {
        if (enabled)
        {
            Stop();
        }
    }

    public void Resume()
    {
        if (enabled)
        {
            Start();
        }
    }

    private void Start()
    {
        var techLists = new List<string[]>();
        switch (NfcType)
        {
            case NfcType.TypeA:
                techLists.Add(new[] { "android.nfc.tech.NfcA" });
                break;
            case NfcType.TypeF:
                techLists.Add(new[] { "android.nfc.tech.NfcF" });
                break;
        }

        var intent = new Intent(activity, activity.GetType()).AddFlags(ActivityFlags.SingleTop);
        nfcAdapter.EnableForegroundDispatch(
            activity,
            PendingIntent.GetActivity(activity, 0, intent, PendingIntentFlags.Mutable),
            new[] { new IntentFilter(NfcAdapter.ActionTechDiscovered) },
            techLists.ToArray());
    }

    private void Stop()
    {
        nfcAdapter.DisableForegroundDispatch(activity);
    }

    public void OnNewIntent(Intent intent)
    {
        if (intent.Action == NfcAdapter.ActionTechDiscovered)
        {
            try
            {
                var idm = intent.GetByteArrayExtra(NfcAdapter.ExtraId)!;
                var tag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag)!;

                var list = tag.GetTechList()!;
                if (list.Any(x => x == "android.nfc.tech.NfcA"))
                {
                    var nfc = NfcA.Get(tag)!;

                    nfc.Connect();
                    subject.OnNext(new AndroidNfcA(idm, nfc));
                }
                if (list.Any(x => x == "android.nfc.tech.NfcF"))
                {
                    var nfc = NfcF.Get(tag)!;

                    nfc.Connect();
                    subject.OnNext(new AndroidNfcF(idm, nfc));
                }
            }
            catch (TagLostException)
            {
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }
    }
}
