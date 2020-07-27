namespace NavigationSample.Modules
{
    using Smart.Forms.ViewModels;

    using XamarinFormsComponents.Popup;

    public class AppDialogViewModelBase : ViewModelBase, IPopupNavigatorAware
    {
        public IPopupNavigator PopupNavigator { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            System.Diagnostics.Debug.WriteLine($"{GetType()} is Disposed");
        }
    }
}
