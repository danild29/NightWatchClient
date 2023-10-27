using NightWatchClientApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;

[QueryProperty(nameof(EventModel), nameof(EventModel))]
public partial class TeamsInEventViewModel : ObservableObject
{
    [ObservableProperty] private EventModel eventModel;
    [ObservableProperty] private string teamId;
    [ObservableProperty] private string message = "";


    private readonly IEventData _eventData;

    public TeamsInEventViewModel(IEventData eventData)
    {
        _eventData = eventData;
    }


    [RelayCommand]
    private async Task AddTeam()
    {
        try
        {
            string id = TeamId.Trim();
            EventModel = await _eventData.AddTeamToEvent(id, EventModel._id);

        }
        catch (Exception ex)
        {

            Message = ex.Message;
        }
    }
    
    [RelayCommand]
    private async Task RemoveTeam(Team selectedTeam)
    {
        try
        {
            if (selectedTeam != null)
            {
                InfoModel message =  await _eventData.KickTeamFromEvent(selectedTeam.teamName, EventModel._id);
                Message = message.message;
                EventModel.members.Remove(selectedTeam); // chteck
            }
        }
        catch (Exception ex)
        {

            Message = ex.Message;
        }
    }

    
}
