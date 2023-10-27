using NightWatchClientApp.Data.Services;

namespace NightWatchClientApp.ViewModels;

public partial class ResetPasswordViewModel : ObservableObject
{
    [ObservableProperty] private string infoMessage;

    [ObservableProperty] private bool isReseted = false;
    [ObservableProperty] private bool isNotReseted = true;
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string eMail = "";
    [ObservableProperty] private NewPasswordDto newPassword = new();

    partial void OnIsResetedChanged(bool oldValue, bool newValue) => IsNotReseted = !newValue;



    private int taskId = 0;

    public IUserData _data { get; }

    public ResetPasswordViewModel(IUserData data)
    {
        _data = data;
    }



    [RelayCommand]
    private async Task ResetPassword()
    {
        try
        {

            var info = await _data.ResetPassword(Name, EMail);
            if (info != null)
            {
                InfoMessage = info.message;
                IsReseted = true;
            }
        }
        catch (Exception ex)
        {
            InfoMessage = ex.Message;
        }
    }

    [RelayCommand]
    private async Task SetNewPassword()
    {
        try
        {

            var info = await _data.SetNewPasssword(Name, NewPassword);
            if (info != null)
            {
                InfoMessage = info.message;
                IsReseted = true;
            }
        }
        catch (Exception ex)
        {
            InfoMessage = ex.Message;
        }
    }

}