using Microsoft.Extensions.Logging;
using NightWatchClientApp.Data.Services;
using NightWatchClientApp.Data.Settings;
using System;
using System.Collections.Generic;
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



    [ObservableProperty] private Team teamModel;
    private string EventId = "6510c8b6f68a88697b733a36";


    public ITeamData _data { get; }
    public IUserData _userData { get; }

    private BackgroundTask updater;
    TimeSpan interval = GlobalSettings.IntervalForUpdatingTeam;

    public MyTeamViewModel(ITeamData data, IUserData userData)
    {
        _data = data;
        _userData = userData;

        ConfigureTimer();
    }

    private async void ConfigureTimer()
    {
        try
        {
            TeamModel = await _userData.GetMyTeam();

            if(TeamModel != null) { 
        
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

        try
        {
            var err = await _data.LogOutTeam();

            ErrorMessage = err.message;
            UserAppInfo.TeamData = null;
            HasTeam = false;
            await updater.StopTask();
        }
        catch (Exception ex)
        {

            ErrorMessage = ex.Message;
            TurnLoginMessage = true;
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
    private async Task GoPlay()
    {
        try
        {
            await Shell.Current.GoToAsync("///" + nameof(PlayPage), true, new Dictionary<string, object>
            {
                {nameof(EventId), EventId }
            });

        }
        catch (Exception ex)
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

    //private void StartGame(object sender, EventArgs e)
    //{


    //}


}