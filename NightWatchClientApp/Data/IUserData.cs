using NightWatchClientApp.Models;

namespace NightWatchClientApp.Data;

public interface IUserData
{
    Task<ErrorModel> Login(UserLoginDto user);
    Task<ErrorModel> Register(UserRegisterDto user);
}