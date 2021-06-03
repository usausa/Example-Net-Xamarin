namespace KeySample.FormsApp.Modules.Control
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using KeySample.FormsApp.Models.Entity;

    using Smart.Collections.Generic;
    using Smart.ComponentModel;
    using Smart.Navigation;

    public class ControlListViewModel : AppViewModelBase
    {
        public ObservableCollection<DataEntity> Items { get; } = new();

        public NotificationValue<string> Selected { get; } = new();

        public ICommand SelectCommand { get; }

        public ControlListViewModel(
            ApplicationState applicationState)
            : base(applicationState)
        {
            Items.AddRange(Enumerable.Range(1, 20).Select(x => new DataEntity { Id = x, Name = $"Name-{x}" }));

            SelectCommand = MakeDelegateCommand<DataEntity>(x => Selected.Value = $"{x.Id} : {x.Name}");
        }

        protected override Task OnNotifyBackAsync() => Navigator.ForwardAsync(ViewId.ControlMenu);

        protected override Task OnNotifyFunction1() => OnNotifyBackAsync();
    }
}
