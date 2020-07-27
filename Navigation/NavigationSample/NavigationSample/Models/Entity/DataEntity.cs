namespace NavigationSample.Models.Entity
{
    public class DataEntity
    {
        public static DataEntity DesignInstance { get; } = null; // For design

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
