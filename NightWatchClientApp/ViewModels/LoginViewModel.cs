using CommunityToolkit.Mvvm.ComponentModel;

using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightWatchClientApp.Data;
using NightWatchClientApp.Models;
using NightWatchClientApp.Views;

namespace NightWatchClientApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{

    public IUserData _userData { get; }

    public LoginViewModel(IUserData userData) {
        _userData = userData;
    }

    [ObservableProperty]
    private string userLoginName = string.Empty;
    [ObservableProperty]
    private string userPassword = string.Empty;
    [ObservableProperty]
    private string loginMessage = string.Empty;
    [ObservableProperty]
    private bool turnLoginMessage = false;


    [RelayCommand]
    private async Task GoToMainPage()
    {
        User user = _userData.Login(UserLoginName, UserPassword);
        if (user != null)
        {
            UserAppInfo.UserData = user;
            LoginMessage = "gooood";
            TurnLoginMessage = true;

            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            LoginMessage = "непраильный имя пользователя или пароль";
            TurnLoginMessage = true;
        }
    }


    [RelayCommand]
    private void GoToCreateAccount(object obj)
    {
        Shell.Current.GoToAsync(nameof(CreateAccountPage));
    }

    [RelayCommand]
    private void GoToForgotPassword(object obj)
    {
        LoginMessage = "эта страница пока не создана";
        TurnLoginMessage = true;
    }

    
}


