using Microsoft.Extensions.Logging;
using NightWatchClientApp.Data.Services;
using NightWatchClientApp.Data.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NightWatchClientApp.ViewModels;

public partial class MyTeamViewModel: ObservableObject
{
    [ObservableProperty] private string errorMessage = string.Empty;
    [ObservableProperty] private bool turnLoginMessage = true;

    [ObservableProperty] private string logTeamName;
    [ObservableProperty] private string logTeamPassword;

    [ObservableProperty] private string createTeamName;
    [ObservableProperty] private string createTeamPassword;

    [ObservableProperty] private bool isBusy = false;
    [ObservableProperty] private bool isVisible = true;
    partial void OnIsBusyChanged(bool oldValue, bool newValue) => IsVisible = !newValue;


    [ObservableProperty] private bool hasTeam = false;
    [ObservableProperty] private bool hasNoTeam = true;
    partial void OnHasTeamChanged(bool oldValue, bool newValue) => HasNoTeam = !newValue;

    [ObservableProperty] private bool hasEvent = false;




    [ObservableProperty] private Team teamModel;
    [ObservableProperty] private EventModel eventModel;


    partial void OnEventModelChanged(EventModel value)
    {
        HasEvent = value != null;
    }



    public ITeamData _data { get; }
    public IUserData _userData { get; }

    private BackgroundTask updater;
    TimeSpan interval = GlobalSettings.IntervalForUpdatingTeam;
    private readonly IEventData eventData;

    public MyTeamViewModel(ITeamData data, IUserData userData, IEventData eventData)
    {
        _data = data;
        _userData = userData;
        this.eventData = eventData;
        ConfigureTimer();
    }

    private async void ConfigureTimer()
    {
        try
        {
            var info = await _userData.GetMyTeam();
    
            if (info != null)
            {
                ErrorMessage = info.message;
                return;
            }

            TeamModel = UserAppInfo.TeamData;
            if (!string.IsNullOrEmpty(TeamModel?.eventName))
                EventModel = await eventData.GetEventByName(TeamModel.eventName);
            if (TeamModel != null) { 
        
                updater = new(interval);
                updater.Start(Update);
                HasTeam = true;
            }

        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
        }

    }

    private async void Update(CancellationToken ct)
    {
        try
        {



            if (await _data.UpdateTeam(ct) == false) return;

            TeamModel = UserAppInfo.TeamData;
            HasTeam = true;
            if(!string.IsNullOrEmpty(TeamModel?.eventName))
                EventModel = await eventData.GetEventByName(TeamModel.eventName);
        }
        catch (OperationCanceledException)
        { 
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            TurnLoginMessage = true;
        }
    }

    [RelayCommand]
    private async Task LogInTeam()
    {
        try
        {
            TeamCreateDto team = new(LogTeamName, LogTeamPassword);

            InfoModel er = await _data.JoinTeam(team);

            if (er == null)
            {
                TeamModel = UserAppInfo.TeamData;
                HasTeam = true;
                updater = new(interval);
                updater.Start(Update);
            }
            else
            {
                ErrorMessage = er.message;
                TurnLoginMessage = true;
            }

        }
        catch (Exception)
        {

            throw;
        }

    }


    [RelayCommand]
    private async Task LogOutTeam()
    {
        InfoModel err = new();
        try
        {
            err = await _data.LeaveTeam();

        }
        catch (Exception ex)
        {
            err = new() { message = ex.Message };
        }    
        finally
        {
            ErrorMessage = err?.message;
            await updater.StopTask();
            TurnLoginMessage = true;

            HasTeam = false;

        }

    }

    [RelayCommand]    
    private async Task CreateTeam()
    {
        try
        {
            TeamCreateDto team = new(CreateTeamName, CreateTeamPassword);

            InfoModel er = await _data.CreateTeam(team);

            if (er == null)
            {
                DataSaver.SaveToDevice(team);
                TeamModel = UserAppInfo.TeamData;
                HasTeam = true;
                updater = new(interval);
                updater.Start(Update);
            }
            else
            {
                ErrorMessage = er.message;
                TurnLoginMessage = true;
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    [RelayCommand]
    private async Task LeaveEvent()
    {

        #warning надо добавить

    }

    [RelayCommand]
    private async Task DetailsTeam()
    {
        await Shell.Current.GoToAsync(nameof(TeamDetailsPage));
    }
    

    [RelayCommand]
    private async Task DetailsEvent()
    {
        if (EventModel != null)
        {
            await Shell.Current.GoToAsync(nameof(EventDetailsPage), true, new Dictionary<string, object>
            {
                {nameof(EventModel), EventModel }
            });
        }
    }

    private DateTime ParseTime(string time)
    {
        try
        {
            string format = "dd.MM.yyyy H:mm:ss";
            return DateTime.ParseExact(time, format, CultureInfo.InvariantCulture);

        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        return default;
    }

    [RelayCommand]
    private async Task GoPlay()
    {
        DateTime start = ParseTime(EventModel.Start);
        DateTime end = ParseTime(EventModel.End);

        if (start == default || end == default) return;
        if (DateTime.UtcNow < start)
        {
            string mess = "событие еще не началось";
            await Shell.Current.DisplayAlert("", mess, "ok");

            return;
        }


        if (DateTime.UtcNow > end)
        {
            string mess = "событие уже закончилось";
            await Shell.Current.DisplayAlert("", mess, "ok");
            return;
        }


        try
        {

            await Shell.Current.GoToAsync("///" + nameof(PlayPage), true, new Dictionary<string, object>
            {
                {nameof(EventId), EventModel._id }
            });

        }
        catch (Exception)
        {

            throw;
        }

    }



    //private void myTeamates_ItemTapped(object sender, ItemTappedEventArgs e)
    //{
    //    User teammate = e.Item as User;
    //    if (teammate == null)
    //        App.Current.MainPage.DisplayAlert("GoToMyTeam", "error with tema", "ok");
    //    else
    //        App.Current.MainPage.DisplayAlert("GoToMyTeam", teammate.Name, "ok");

    //}




}