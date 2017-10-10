namespace Inventory.Client
{
    using Inventory.Client.Components;

    public static class SettingServiceExtensions
    {
        // Network

        private const string EndPointKey = "EndPoint";

        public static string GetEndPoint(this ISettingService setting)
        {
            return setting.GetString(EndPointKey, "http://server/");
        }

        public static void SetEndPoint(this ISettingService setting, string value)
        {
            setting.SetString(EndPointKey, value);
        }

        // Terminal

        private const string TerminalNoKey = "TerminalNo";

        public static string GetTerminalNo(this ISettingService setting)
        {
            // TODO
            return setting.GetString(TerminalNoKey, "999999");
        }

        public static void SetTerminalNo(this ISettingService setting, string value)
        {
            setting.SetString(TerminalNoKey, value);
        }
    }
}
