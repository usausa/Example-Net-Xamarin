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
            try
            {
                return nfc.Transceive(command);
            }
            catch (TagLostException)
            {
                return Array.Empty<byte>();
            }
        }
    }
}