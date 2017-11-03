namespace FeliCaReader.FormsApp.Droid.Services
{
    using System;

    using Android.Nfc;
    using Android.Nfc.Tech;

    using FeliCaReader.FormsApp.Services;

    public class AndroidFeliCaReader : IFeliCaReader
    {
        private readonly NfcF nfc;

        public AndroidFeliCaReader(NfcF nfc)
        {
            this.nfc = nfc;
        }

        public byte[] Access(byte[] command)
        {
            System.Diagnostics.Debug.WriteLine("SEND: " + BitConverter.ToString(command));
            try
            {
                var response = nfc.Transceive(command);
                System.Diagnostics.Debug.WriteLine("RECV: " + BitConverter.ToString(response));
                return response;
            }
            catch (TagLostException)
            {
                System.Diagnostics.Debug.WriteLine("RECV: ");
                return Array.Empty<byte>();
            }
        }
    }
}