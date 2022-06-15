namespace BluetoothSample.FormsApp.Components.Printer
{
    using System.Threading.Tasks;

    public interface IPrinter
    {
        ValueTask<bool> WriteAsync(string command);
    }
}
