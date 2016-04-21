namespace Example.FormsApp.Models
{
    using Example.ComponentModel;

    public class DataEntity : NotificationObject
    {
        private int id;

        private string name;

        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
    }
}
