using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;

public partial class ProfileViewModel: ObservableObject
{
    [ObservableProperty]
    public string name = UserAppInfo.UserData.FullName;


    public IUserData _userData { get; }

    public ProfileViewModel(IUserData userData)
    {
        _userData = userData;
    }


    [RelayCommand]
    private async Task LogOut()
    {
        UserAppInfo.UserData = null;
        Preferences.Default.Remove(nameof(UserLoginDto));

        await Shell.Current.GoToAsync("///" + nameof(LoginPage));
    }

    [RelayCommand]
    private void GoToSettings()
    {
        Name = UserAppInfo.UserData.FullName;
    }

    [RelayCommand]
    private void GoToAboutProgram()
    {
        //App.Current.MainPage.Navigation.PushAsync(new AboutProgramPage());
    }

    [RelayCommand]
    private void GetPremium()
    {
        if (UserAppInfo.UserData.IsPremium == true)
        {
            App.Current.MainPage.DisplayAlert("уже есть", "уже есть", "ok");
        }
        else
        {
            UserAppInfo.UserData.IsPremium = true;
        }
    }
}
