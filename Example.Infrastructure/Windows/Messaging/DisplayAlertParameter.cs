namespace Example.Windows.Messaging
{
    using System.Threading.Tasks;

    public class DisplayAlertParameter
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public string Accept { get; set; }

        public string Cancel { get; set; }

        public Task<bool> Result { get; set; }
    }
}
