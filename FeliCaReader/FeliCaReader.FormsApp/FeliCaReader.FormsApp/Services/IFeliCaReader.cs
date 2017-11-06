namespace FeliCaReader.FormsApp.Services
{
    public interface IFeliCaReader
    {
        byte[] Access(byte[] command);
    }
}
