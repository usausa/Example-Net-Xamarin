namespace Example.FormsApp.Models
{
    using System.Diagnostics;

    public class ApplicationState
    {
        public int Counter { get; set; }

        public ApplicationState()
        {
            Debug.WriteLine("ApplicationState created.");
        }
    }
}
