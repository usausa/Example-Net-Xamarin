namespace Example.Windows.Messaging
{
    /// <summary>
    ///
    /// </summary>
    public class BusyIndicatorParameter
    {
        public static BusyIndicatorParameter True { get; } = new BusyIndicatorParameter(true);

        public static BusyIndicatorParameter False { get; } = new BusyIndicatorParameter(false);

        /// <summary>
        ///
        /// </summary>
        public bool IsBusy { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isBusy"></param>
        public BusyIndicatorParameter(bool isBusy)
        {
            IsBusy = isBusy;
        }
    }
}
