using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NightWatchClientApp.Data.Services;

namespace NightWatchClientApp.ViewModels;

public partial class ProfileViewModel: ObservableObject
{
    [ObservableProperty] public string name = UserAppInfo.UserData.Name;
    [ObservableProperty] public string message = "";

    

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
        
    }

    [RelayCommand]
    private void GoToAboutProgram()
    {
        //App.Current.MainPage.Navigation.PushAsync(new AboutProgramPage());
    }

    [RelayCommand]
    private async Task GetPremium()
    {

        if (UserAppInfo.UserData.roles is not null && UserAppInfo.UserData.roles.Contains("vip"))
        {
            await App.Current.MainPage.DisplayAlert("уже есть", "уже есть", "ok");
        }
        else
        {
            InfoModel info = await _userData.GetVip(UserAppInfo.UserData._id);
            Message = info.message;
            await Application.Current.MainPage.DisplayAlert(Message, "перезайдите в аккаунт чтобы получить все привелегии", "ok");
            await LogOut();
        }
    }
}
