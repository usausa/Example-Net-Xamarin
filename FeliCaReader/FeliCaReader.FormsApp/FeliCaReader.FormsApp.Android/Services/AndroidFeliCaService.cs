namespace FeliCaReader.FormsApp.Droid.Services
{
    using System;
    using System.Reactive.Subjects;

    using Android.Content;
    using Android.Nfc;
    using Android.Nfc.Tech;

    using FeliCaReader.FormsApp.Services;

    public class AndroidFeliCaService : IFeliCaService
    {
        private readonly Subject<IFeliCaReader> subject = new Subject<IFeliCaReader>();

        public IObservable<IFeliCaReader> Detected => subject;

        public void OnNewIntent(Intent intent)
        {
            var tag = (Tag)intent.GetParcelableExtra(NfcAdapter.ExtraTag);
            var nfc = NfcF.Get(tag);
            try
            {
                nfc.Timeout = 50;
                nfc.Connect();
                subject.OnNext(new AndroidFeliCaReader(nfc));
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