namespace Example.Windows.Interactivity
{
    using Xamarin.Forms;

    public class SetLabelTextMessageAction : MessageAction<Label>
    {
        protected override void Invoke(Label associatedObject, object parameter)
        {
            associatedObject.Text = parameter.ToString();
        }
    }
}
