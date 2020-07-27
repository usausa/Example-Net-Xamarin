namespace NavigationSample.Modules.Modal
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using NavigationSample.Models.Input;

    using XamarinFormsComponents.Popup;

    public class InputNumberViewModel : AppDialogViewModelBase, IPopupResult<string>, IPopupInitialize<NumberInputParameter>
    {
        public static InputNumberViewModel DesignInstance { get; } = null; // For design

        public NumberInputModel Input { get; } = new NumberInputModel();

        public string Result { get; private set; }

        public ICommand ClearCommand { get; }
        public ICommand PopCommand { get; }
        public ICommand PushCommand { get; }

        public ICommand CloseCommand { get; }
        public ICommand CommitCommand { get; }

        public InputNumberViewModel()
        {
            ClearCommand = MakeDelegateCommand(() => Input.Clear());
            PopCommand = MakeDelegateCommand(() => Input.Pop());
            PushCommand = MakeDelegateCommand<string>(x => Input.Push(x));

            CloseCommand = MakeAsyncCommand(Close);
            CommitCommand = MakeAsyncCommand(Commit);
        }

        public void Initialize(NumberInputParameter parameter)
        {
            Input.Text = parameter.Value;
            Input.MaxLength = parameter.MaxLength;
            Input.Scale = parameter.Scale;
            Input.AllowEmpty = parameter.AllowEmpty;
        }

        private async Task Close()
        {
            await PopupNavigator.PopAsync();
        }

        private async Task Commit()
        {
            Result = Input.Text;

            await PopupNavigator.PopAsync();
        }
    }
}
