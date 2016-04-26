namespace Example.Windows.Messaging
{
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public class DisplayActionSheetParameter
    {
        /// <summary>
        ///
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Cancel { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Destruction { get; set; }

        /// <summary>
        ///
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Ignore")]
        public string[] Buttons { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Task<string> Result { get; set; }
    }
}
