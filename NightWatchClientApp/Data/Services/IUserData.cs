using NightWatchClientApp.Models;

namespace NightWatchClientApp.Data.Services;

public interface IUserData
{
    Task<InfoModel> Login(UserLoginDto user);
    Task<InfoModel> Register(UserRegisterDto user);
    Task<Team> GetMyTeam();
}