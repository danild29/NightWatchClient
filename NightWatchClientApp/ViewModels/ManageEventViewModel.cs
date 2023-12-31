﻿using NightWatchClientApp.Data.Services;


namespace NightWatchClientApp.ViewModels;


[QueryProperty(nameof(EventModel), nameof(EventModel))]
public partial class ManageEventViewModel : ObservableObject
{
    [ObservableProperty] private EventModel eventModel;
    [ObservableProperty] private string errorMessage = string.Empty;

    [ObservableProperty] private TaskModel newQuestion = new TaskModel();


    public readonly IEventData _eventData;

    public ManageEventViewModel(IEventData eventData)
    {
        _eventData = eventData;
    }

    [RelayCommand]
    private async Task CreateTask()
    {
        try
        {
            var question = await _eventData.AddQuestionToEvent(EventModel._id, NewQuestion);

            NewQuestion = new TaskModel();

        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }
    }


    [RelayCommand]
    private async Task GoToTeamsInEvent()
    {
        if (EventModel == null) throw new Exception("EventModel can't be null here (GoToTeamsInEvent)");

        await Shell.Current.GoToAsync(nameof(TeamsInEventPage), true, new Dictionary<string, object>
        {
            {nameof(EventModel), EventModel }
        });
    }
}