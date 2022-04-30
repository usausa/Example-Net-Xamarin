namespace KeySample.FormsApp.Modules
{
    using Smart.Forms.ViewModels;

    using XamarinFormsComponents.Popup;

    public class AppDialogViewModelBase : ViewModelBase, IPopupNavigatorAware
    {
        public IPopupNavigator PopupNavigator { get; set; } = default!;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }
    }
}
