using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;

public partial class TeamDetailsViewModel: ObservableObject
{
    [ObservableProperty] public Team teamModel;

    public TeamDetailsViewModel()
    {
        TeamModel = UserAppInfo.TeamData;
    }


}
