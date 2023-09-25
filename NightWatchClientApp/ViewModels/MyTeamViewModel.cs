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
    [ObservableProperty] private bool turnLoginMessage = false;

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



    [ObservableProperty]
    private Team teamModel;



    public ITeamData _data { get; }
    private BackgroundTask updater;
    TimeSpan interval = GlobalSettings.IntervalForUpdatingTeam;

    public MyTeamViewModel(ITeamData data)
    {
        _data = data;
        updater = new(interval);

        if(DataSaver.GetFromDevice<Team>() is not null)
        {
            updater.Start(Update);
        }
    }
    private async void Update(CancellationToken ct)
    {
        try
        {
            if (UserAppInfo.TeamData == null)
            {
                UserAppInfo.TeamData = DataSaver.GetFromDevice<Team>();
            }

            if (await _data.UpdateTeam(ct) == false) return;

            DataSaver.SaveToDevice(UserAppInfo.TeamData);
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
        TeamCreateDto team = new(LogTeamName, LogTeamPassword);

        ErrorModel er = await _data.JoinTeam(team);

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


    [RelayCommand]
    private async Task LogOutTeam()
    {

        try
        {
            await _data.LogOutTeam();
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
        TeamCreateDto team = new(CreateTeamName, CreateTeamPassword);

        ErrorModel er = await _data.CreateTeam(team);

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