namespace NfcSample.FormsApp.Helpers;

using System;
using System.Collections.Generic;
using System.IO;

public sealed class SettingParser
{
    private readonly Dictionary<string, string> values = new(StringComparer.OrdinalIgnoreCase);

    public SettingParser(string data)
    {
        using var reader = new StringReader(data);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            var index = line.IndexOf('=', StringComparison.Ordinal);
            if (index > 0)
            {
                values[line[..index].Trim()] = line[(index + 1)..].Trim();
            }
        }
    }

    public bool TryGetString(string key, out string value) => values.TryGetValue(key, out value);

    public string? GetString(string key) => values.TryGetValue(key, out var value) ? value : default;

    public string GetString(string key, string defaultValue) => values.TryGetValue(key, out var value) ? value : defaultValue;

    public bool TryGetInt(string key, out int value)
    {
        if (values.TryGetValue(key, out var temp))
        {
            return Int32.TryParse(temp, out value);
        }

        value = 0;
        return false;
    }

    public int GetInt(string key) => values.TryGetValue(key, out var value) && Int32.TryParse(value, out var result) ? result : default;

    public int GetInt(string key, int defaultValue) => values.TryGetValue(key, out var value) && Int32.TryParse(value, out var result) ? result : defaultValue;
}
