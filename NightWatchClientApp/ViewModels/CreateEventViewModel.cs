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


    [ObservableProperty] private DateTime selectedDateStart = DateTime.UtcNow;
    [ObservableProperty] private DateTime selectedDateEnd = DateTime.UtcNow;

    [ObservableProperty] private TimeSpan selectedTimeStart;
    [ObservableProperty] private TimeSpan selectedTimeEnd;

    
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

        Task.Run(LoadEvents);

        //managedEvents = new ObservableCollection<EventModel>(_eventData.EventManager.Events);
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

    private static DateTime ConvertToOneTime(DateTime timeDate, TimeSpan timeSpan)
    {
        return new DateTime(timeDate.Year, timeDate.Month, timeDate.Day, timeSpan.Hours, timeSpan.Minutes, 0);
    }

    [RelayCommand]
    private async Task CreateEvent()
    {
        try
        {

            
            var start = ConvertToOneTime(SelectedDateStart, SelectedTimeStart).ToString().ToString();
            var end = ConvertToOneTime(SelectedDateEnd, SelectedTimeEnd).ToString().ToString();

            var dto = new CreateEventDto(EventName, EventDescription, start, end);

            var e = await _eventData.CreateEvent(dto);

            AquiredEvent(true);
            ManagedEvents.Add(e);
            Error = "";

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
