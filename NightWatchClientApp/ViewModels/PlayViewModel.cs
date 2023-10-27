using Microsoft.Extensions.Logging;
using NightWatchClientApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;


public partial class PlayViewModel: ObservableObject, IQueryAttributable
{
    private string WrongAnswer = "Неправильный ответ.";
    private string CorrectAnswer = "Правильный ответ.";
    [ObservableProperty] private string infoMessage;

    [ObservableProperty] private Team teamModel;
    [ObservableProperty] private EventModel eventModel;
    [ObservableProperty] private TaskModel currentTask;
    [ObservableProperty] private bool isCompleted;
    [ObservableProperty] private bool isNotCompleted;

    partial void OnIsCompletedChanged(bool oldValue, bool newValue) => IsNotCompleted = !newValue;

    [ObservableProperty] private string answer;
    private string EventId;


    private int taskId = 0;

    public ITeamData _data { get; }
    public IEventData _eventData { get; }

    public PlayViewModel(ITeamData data, IEventData eventData)
    {
        _data = data;
        _eventData = eventData;

    }

    private async Task Init()
    {
        try
        {
            EventModel = await _eventData.GetEvent(EventId);

            if(taskId < EventModel.questions.Count)
            {
                ShowWinner();
            }
            CurrentTask = EventModel.questions[taskId];
            TeamModel = UserAppInfo.TeamData ?? throw new Exception("UserAppInfo.TeamData is null");

        }
        catch (Exception ex)
        {
            InfoMessage = ex.Message;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }


    [RelayCommand]
    private async Task SendAnswer()
    {
        try
        {

            var info = await _data.AnswerQuestion(Answer, CurrentTask._id, EventModel._id);
            var m = info.message;
            InfoMessage = m;

            if(m == CorrectAnswer)
            {
                Answer = "";
                taskId++;
                if(taskId < EventModel.questions.Count)
                {
                    CurrentTask = EventModel.questions[taskId];
                }
                else
                {
                    
                    ShowWinner();
                }
            }
            else if(m == WrongAnswer)
            {

            }
            else
            {
                throw new Exception("InfoMessage: " + m);
            }


        }
        catch (Exception ex)
        {
            InfoMessage = "ошибка: " + ex.Message;
        }
    }

    private void ShowWinner()
    {
        IsCompleted = true;

        InfoMessage = "вы прошли квест";
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        EventId = (string)query[nameof(EventId)];
        Task t = Init();

    }
}
