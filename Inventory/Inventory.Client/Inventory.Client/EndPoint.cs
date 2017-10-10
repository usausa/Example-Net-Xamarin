namespace Inventory.Client
{
    using System;

    public static class EndPoint
    {
        public static string MasterItem(string baseUrl)
        {
            return String.Concat(baseUrl, "api/master/item");
        }

        public static string StorageList(string baseUrl)
        {
            return String.Concat(baseUrl, "api/storage/list");
        }

        public static string StorageDetails(string baseUrl, int id)
        {
            return String.Concat(baseUrl, "api/storage/details/", id);
        }

        public static string StorageDetails(string baseUrl)
        {
            return String.Concat(baseUrl, "api/storage/details");
        }
    }
}
