using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;

public partial class CreateAccountViewModel : ObservableObject
{
    public IUserData _userData { get; }

    public CreateAccountViewModel(IUserData userData)
    {
        _userData = userData;
    }

    [ObservableProperty]
    private string userLoginName;
    [ObservableProperty]
    private string userPassword;
    [ObservableProperty]
    private string userNickName;
    [ObservableProperty]
    private string loginMessage;
    [ObservableProperty]
    private bool turnLoginMessage = false;

    

    [RelayCommand]
    private async Task RegisterNewUser()
    {
        User user = new User(UserLoginName, UserPassword, UserNickName);

        if (_userData.Register(user))
        {
            UserAppInfo.UserData = user;

            await Shell.Current.GoToAsync("//MainPage");

        }
        else
        {
            LoginMessage = "такой пользователь уже зарегестирован";
            TurnLoginMessage = true;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}
