using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NightWatchClientApp.Data.Services;

namespace NightWatchClientApp.ViewModels;

public partial class CreateEventViewModel: ObservableObject
{
    [ObservableProperty] private string error = string.Empty;

    [ObservableProperty] private ObservableCollection<EventModel> managedEvents;

    [ObservableProperty] private string eventName;
    [ObservableProperty] private string eventDescription;



    [ObservableProperty] private bool haveEvent = false;
    [ObservableProperty] private bool noHaveEvent = true;

    void AquiredEvent(bool isHaveEvent)
    {
        HaveEvent = isHaveEvent;
        NoHaveEvent = !isHaveEvent;
    }

    public readonly IEventData _eventData;

    public CreateEventViewModel(IEventData eventData)
    {
        _eventData = eventData;
    }



    [RelayCommand]
    private async Task LoadEvents()
    {
        try
        {
            var loadedEvent = (await _eventData.GetEventsByHostId(UserAppInfo.UserData._id)).ToList();
            ManagedEvents = new ObservableCollection<EventModel>(loadedEvent);
            AquiredEvent(true);
        }
        catch (Exception ex)
        {

            Error = ex.Message;
        }
    }

    [RelayCommand]
    private async Task CreateEvent()
    {
        try
        {
            
            var e = await _eventData.CreateEvent(EventName, EventDescription);

            AquiredEvent(true);
            ManagedEvents.Add(e);

        }
        catch (Exception ex)
        {

            Error = ex.Message;
        }
    }


    [RelayCommand]
    private async Task GoToManageEvent(EventModel selectedEvent)
    {
        if (selectedEvent != null)
        {
            await Shell.Current.GoToAsync(nameof(ManageEventPage), true, new Dictionary<string, object>
            {
                {nameof(EventModel), selectedEvent }
            });
        }
    }




}
