using NightWatchClientApp.Models;

namespace NightWatchClientApp.Data.Services;

public interface IUserData
{
    Task<InfoModel> Login(UserLoginDto user);
    Task<InfoModel> Register(UserRegisterDto user);
    Task<InfoModel> GetMyTeam();
    Task<InfoModel> GetVip(string userid, UserLoginDto user);
    Task<InfoModel> ResetPassword(string name, string eMail);
    Task<InfoModel> SetNewPasssword(string name, NewPasswordDto newPassword);
}


