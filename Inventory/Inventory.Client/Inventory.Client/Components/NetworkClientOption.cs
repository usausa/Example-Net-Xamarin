namespace Inventory.Client.Components
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class NetworkClientOption
    {
        public JsonSerializerSettings SerializerSettings { get; set; } = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateFormatHandling = DateFormatHandling.IsoDateFormat
        };
    }
}
