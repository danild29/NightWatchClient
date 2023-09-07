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

    [ObservableProperty] private string logTeamId;
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

    public MyTeamViewModel(ITeamData data)
    {
        _data = data;
    }

    [RelayCommand]
    private async Task LogInTeam()
    {
        

    }







    [RelayCommand]    
    private async Task CreateTeam()
    {
        TeamCreateDto team = new(CreateTeamName, CreateTeamPassword);

        ErrorModel er = await _data.CreateTeam(team);

        if (er == null)
        {
            Preferences.Default.Set(nameof(UserLoginDto), JsonSerializer.Serialize(team));
            TeamModel = UserAppInfo.TeamData;
            HasTeam = true;
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