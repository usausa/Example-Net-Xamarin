namespace Example.FormsApp.Interactivity
{
    using Example.Windows.Interactivity;

    using Xamarin.Forms;

    public class SetTextMessageAction : MessageAction<Label>
    {
        protected override void Invoke(Label associatedObject, object parameter)
        {
            associatedObject.Text = parameter.ToString();
        }
    }
}
