using NightWatchClientApp.Models;

namespace NightWatchClientApp.Data;

public interface IUserData
{
    User Login(string username, string password);
    bool Register(User user);
}