namespace FeliCaReader.FormsApp.Services
{
    using System;

    public interface IFeliCaService
    {
        IObservable<IFeliCaReader> Detected { get; }
    }
}
