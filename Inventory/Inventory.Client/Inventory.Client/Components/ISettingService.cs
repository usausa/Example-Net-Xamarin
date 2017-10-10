namespace Inventory.Client.Components
{
    public interface ISettingService
    {
        string GetString(string key, string defaultValue);

        int GetInteger(string key, int defaultValue);

        long GetLong(string key, long defaultValue);

        void SetString(string key, string value);

        void SetInteger(string key, int value);

        void SetLong(string key, long value);
    }
}
