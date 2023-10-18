using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NightWatchClientApp.Data.Services;

namespace NightWatchClientApp.ViewModels;

public partial class CreateEventViewModel: ObservableObject
{
    [ObservableProperty] private string error = string.Empty;

    [ObservableProperty] private ObservableCollection<EventModel> managedEvents = new();

    [ObservableProperty] private string eventName;
    [ObservableProperty] private string eventDescription;
    [ObservableProperty] private string startTime = "";
    [ObservableProperty] private string endTime = "";



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
        managedEvents = new ObservableCollection<EventModel>(_eventData.EventManager.Events);
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

            if(string.IsNullOrEmpty(StartTime)) StartTime = DateTime.UtcNow.ToString();
            if(string.IsNullOrEmpty(EndTime)) EndTime = (DateTime.UtcNow + TimeSpan.FromDays(10)).ToString();


            var dto = new CreateEventDto(EventName, EventDescription, StartTime, EndTime);

            var e = await _eventData.CreateEvent(dto);

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
