namespace KeySample.FormsApp.Modules.Popup
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using KeySample.FormsApp.Components.Dialog;

    using KeySample.FormsApp.Models.Input;

    using Smart.ComponentModel;

    using XamarinFormsComponents.Popup;

    public class PopupType1ViewModel : AppDialogViewModelBase, IPopupResult<string>, IPopupInitialize<TextInputParameter>
    {
        private readonly IApplicationDialog dialogs;

        [AllowNull]
        private string currentText;

        public NotificationValue<string> Title { get; } = new();

        public TextInputModel Input { get; } = new();

        public string Result { get; private set; } = string.Empty;

        public ICommand ClearCommand { get; }
        public ICommand PopCommand { get; }
        public ICommand PushCommand { get; }

        public ICommand CloseCommand { get; }
        public ICommand CommitCommand { get; }

        public PopupType1ViewModel(
            IApplicationDialog dialogs)
        {
            this.dialogs = dialogs;

            ClearCommand = MakeDelegateCommand(() => Input.Clear());
            PopCommand = MakeDelegateCommand(() => Input.Pop());
            PushCommand = MakeDelegateCommand<string>(x => Input.Push(x));

            CloseCommand = MakeAsyncCommand(Close);
            CommitCommand = MakeAsyncCommand(Commit);
        }

        public void Initialize(TextInputParameter parameter)
        {
            Title.Value = parameter.Title;
            Input.MaxLength = parameter.MaxLength;
            Input.Text = parameter.Value;
            currentText = Input.Text;
        }

        private async Task Close()
        {
            if ((currentText != Input.Text) &&
                (!await dialogs.Confirm("入力した内容をキャンセルし戻ります。よろしいですか？")))
            {
                return;
            }

            await PopupNavigator.PopAsync();
        }

        private async Task Commit()
        {
            Result = Input.Text;

            await PopupNavigator.PopAsync();
        }
    }
}
