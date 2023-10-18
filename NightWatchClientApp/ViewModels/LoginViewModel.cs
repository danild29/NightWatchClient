using CommunityToolkit.Mvvm.ComponentModel;

using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightWatchClientApp.Models;
using NightWatchClientApp.Views;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Text.Json;
using NightWatchClientApp.Data.Services;

namespace NightWatchClientApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{

    public IUserData _userData { get; }

    public LoginViewModel(IUserData userData) 
    {
        _userData = userData;

    }
    [ObservableProperty] private string userLoginName = string.Empty;
    [ObservableProperty] private string userPassword = string.Empty;
    [ObservableProperty] private string loginMessage = string.Empty;
    [ObservableProperty] private bool turnLoginMessage = false;

    [ObservableProperty] private bool isBusy = false;
    [ObservableProperty] private bool isVisible = true;
    partial void OnIsBusyChanged(bool oldValue, bool newValue) => IsVisible = !newValue;

    public async Task GetDataFromPrefernces()
    {
        //DataSaver.ClearAll();
        IsBusy = true;
        try
        {
            string data = Preferences.Default.Get<string>(nameof(UserLoginDto), null);
            if (data == null) return;

            var user = JsonSerializer.Deserialize<UserLoginDto>(data);

            InfoModel er = await _userData.Login(user);
            if (er == null)
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                LoginMessage = er.message;
                TurnLoginMessage = true;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("LoginViewModel", ex.Message, "ok");
        }
        finally 
        {
            IsBusy = false; 
        }
    
    }

    


    [RelayCommand]
    private async Task GoToMainPage()
    {
        IsBusy = true;

        UserLoginDto user = new(UserLoginName, UserPassword);
        try
        {
            InfoModel er = await _userData.Login(user);
            if (er == null)
            {
                Preferences.Default.Set(nameof(UserLoginDto), JsonSerializer.Serialize(user));
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                LoginMessage = er.message;
                TurnLoginMessage = true;
            }
        }
        catch (Exception ex)
        {
            LoginMessage = ex.Message;
            TurnLoginMessage = true;
        }
        

        IsBusy = false;
    }


    [RelayCommand]
    private void GoToCreateAccount(object obj)
    {
        // obj is null?
        Shell.Current.GoToAsync(nameof(CreateAccountPage));
    }

    [RelayCommand]
    private void GoToForgotPassword(object obj)
    {
        LoginMessage = "эта страница пока не создана";
        TurnLoginMessage = true;
    }

    
}


