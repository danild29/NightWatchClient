using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NightWatchClientApp.Helpers;

internal class DataSaver
{

    public static void SaveToDevice<T>(T data)
    {
        Preferences.Default.Set(nameof(T), JsonSerializer.Serialize(data));
    }

    public static T GetFromDevice<T>()
    {
        string data = Preferences.Default.Get<string>(nameof(T), null);
        return JsonSerializer.Deserialize<T>(data);
    }

}
