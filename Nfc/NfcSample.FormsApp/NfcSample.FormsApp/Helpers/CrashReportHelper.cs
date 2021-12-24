namespace NfcSample.FormsApp.Helpers;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;

public static class CrashReportHelper
{
    [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Ignore")]
    public static void LogException(Exception e)
    {
        try
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "dump.log");

            var log = new StringBuilder();
            log.AppendLine($"Time: {DateTime.Now:yyyy/MM/dd HH:mm:ss}");
            log.AppendLine("Exception:");
            log.AppendLine(e.ToString());

            File.WriteAllText(path, log.ToString());
        }
        catch
        {
            // Ignore
        }
    }

    public static async ValueTask ShowReport()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "dump.log");

        if (!File.Exists(path))
        {
            return;
        }

        var log = await File.ReadAllTextAsync(path);

        await Application.Current.MainPage.DisplayAlert("Crash report", log, "Close");

        File.Delete(path);
    }

    public static string? GetReport()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, "dump.log");

        return !File.Exists(path) ? null : File.ReadAllText(path);
    }
}
