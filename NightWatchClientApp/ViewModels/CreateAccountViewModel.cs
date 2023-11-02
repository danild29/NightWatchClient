using CommunityToolkit.Mvvm.ComponentModel;
using NightWatchClientApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NightWatchClientApp.ViewModels;

public partial class CreateAccountViewModel : ObservableObject
{
    public IUserData _userData { get; }

    public CreateAccountViewModel(IUserData userData)
    {
        _userData = userData;
    }

    [ObservableProperty] private string userLoginName;
    [ObservableProperty] private string userPassword;
    [ObservableProperty] private string userRepeatPassword;
    [ObservableProperty] private string userNickName;
    [ObservableProperty] private string loginMessage = "";
    [ObservableProperty] private bool turnLoginMessage = false;

    [ObservableProperty] private bool isBusy = false;
    [ObservableProperty] private bool isVisible = true;
    partial void OnIsBusyChanged(bool oldValue, bool newValue) => IsVisible = !newValue;

    [RelayCommand]
    private async Task RegisterNewUser()
    {
        if(UserPassword != UserRepeatPassword)
        {
            LoginMessage = "пароли не совпадают";
            TurnLoginMessage = true;
            return;
        }

        LoginMessage = "";
        IsBusy = true;
        UserRegisterDto user = new UserRegisterDto(UserLoginName, UserPassword, UserNickName);

        InfoModel er = await _userData.Register(user);
        if (er == null)
        {
            UserLoginDto u = new UserLoginDto(UserNickName, UserPassword);
            Preferences.Default.Set(nameof(UserLoginDto), JsonSerializer.Serialize(u));

            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            try
            {
                StringBuilder mes = new();
            
                string ad = er.message;

                foreach(var i in er.errors.errors)
                {
                    ad += Environment.NewLine +  i.msg;
                }

                var e = er.errors.errors.FirstOrDefault();
                if (LoginMessage != null)
                {
                    LoginMessage = e.msg;
                    TurnLoginMessage = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        IsBusy = false;
    }



    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

}
