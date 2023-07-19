using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;

public partial class ProfileViewModel: ObservableObject
{
    public string Name => UserAppInfo.UserData.FullName;

    public IUserData _userData { get; }

    public ProfileViewModel(IUserData userData)
    {
        _userData = userData;
    }

    [RelayCommand]
    private async void LogOut()
    {
        UserAppInfo.UserData = null;

        await Shell.Current.GoToAsync(nameof(LoginPage));
        
        //App.Current.MainPage = new NavigationPage(new LoginPage());
    }

    [RelayCommand]
    private void GoToSettings()
    {
        //App.Current.MainPage.Navigation.PushAsync(new AboutProgramPage());
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
