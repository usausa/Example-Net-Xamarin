namespace Inventory.Client.Components
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public class SettingService : ISettingService
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public string GetString(string key, string defaultValue)
        {
            return AppSettings.GetValueOrDefault(key, defaultValue);
        }

        public int GetInteger(string key, int defaultValue)
        {
            return AppSettings.GetValueOrDefault(key, defaultValue);
        }

        public long GetLong(string key, long defaultValue)
        {
            return AppSettings.GetValueOrDefault(key, defaultValue);
        }

        public void SetString(string key, string value)
        {
            AppSettings.AddOrUpdateValue(key, value);
        }

        public void SetInteger(string key, int value)
        {
            AppSettings.AddOrUpdateValue(key, value);
        }

        public void SetLong(string key, long value)
        {
            AppSettings.AddOrUpdateValue(key, value);
        }
    }
}
