namespace FeliCaReader.FormsApp.Pages
{
    using System;
    using System.Reactive.Linq;
    using System.Threading;

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
            // TODO Polling & basic & sflog
        }
    }
}
