

namespace NightWatchClientApp.Models;


public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
    public bool IsPremium { get; set; }

    public int? TeamId { get; set; } = null;

    public User()
    {
        
    }

    public User(string email, string password, string name)
    {
        Email = email;
        Password = password;
        Name = name;
        IsPremium = false;
    }
}

