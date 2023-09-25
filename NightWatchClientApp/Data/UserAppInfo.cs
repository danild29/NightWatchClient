using NightWatchClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.Data;

public static class UserAppInfo
{
    public static User UserData { get; set; }
    public static Team TeamData { get; set; }


    public static void Save()
    {
        DataSaver.SaveToDevice(UserData);
        DataSaver.SaveToDevice(TeamData);
    }

    public static void Load()
    {
        UserData = DataSaver.GetFromDevice<User>();
        TeamData = DataSaver.GetFromDevice<Team>();
    }


}
