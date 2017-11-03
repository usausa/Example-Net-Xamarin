namespace FeliCaReader.FormsApp.Models
{
    using System;

    public class SuicaLogData
    {
        public byte TerminalType { get; set; }

        public byte ProcessType { get; set; }

        public bool WithCash { get; set; }

        public DateTime DateTime { get; set; }

        public int Balance { get; set; }

        public int TransactionId { get; set; }
    }
}
