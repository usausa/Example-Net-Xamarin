namespace BluetoothSample.FormsApp
{
    using System.Threading.Tasks;

    using Xamarin.Essentials;

    public static class Permissions
    {
        public static async ValueTask<bool> IsPermissionRequired()
        {
            var status = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                return true;
            }

            status = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                return true;
            }

            return false;
        }

        public static async ValueTask<bool> RequestPermissions()
        {
            var status = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                return false;
            }

            status = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                return false;
            }

            return true;
        }
    }
}
