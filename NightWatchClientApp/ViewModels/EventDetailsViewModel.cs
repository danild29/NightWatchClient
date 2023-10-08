using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;


[QueryProperty(nameof(EventModel), nameof(EventModel))]
public partial class EventDetailsViewModel: ObservableObject
{
    [ObservableProperty] private EventModel eventModel;


    public EventDetailsViewModel()
    {
        var a = EventModel;
    }

    [RelayCommand]
    private async Task JoinEvent()
    {
       
    }
}
