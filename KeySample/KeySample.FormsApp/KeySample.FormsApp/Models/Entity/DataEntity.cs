namespace KeySample.FormsApp.Models.Entity
{
    using System.Diagnostics.CodeAnalysis;

    public class DataEntity
    {
        public int Id { get; set; }

        [AllowNull]
        public string Name { get; set; }
    }
}
