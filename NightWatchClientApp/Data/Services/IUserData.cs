using NightWatchClientApp.Models;

namespace NightWatchClientApp.Data.Services;

public interface IUserData
{
    Task<ErrorModel> Login(UserLoginDto user);
    Task<ErrorModel> Register(UserRegisterDto user);
}