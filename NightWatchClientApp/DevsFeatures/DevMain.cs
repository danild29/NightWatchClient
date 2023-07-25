using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.DevsFeatures;

public static class DevMain
{
    public static bool IsDev = true;
    public static bool IsFastLogin = true;

    public static List<string> problems = new List<string>();


    private const string logFile = @"C:\Users\Dania\source\repos\NightWatchClient\NightWatchClientApp\DevsFeatures\logFile.txt";

    public static void WriteToLogs(string message)
    {

        if (!File.Exists(logFile))
        {
            File.WriteAllText(logFile, "");
        }
        List<string> file = File.ReadAllLines(logFile).ToList();
        file.Add(DateTime.Now + message + "\n");
        file.Add(new string(' ', 20) + "\n");
        File.WriteAllLines(logFile, file);
    }
}
