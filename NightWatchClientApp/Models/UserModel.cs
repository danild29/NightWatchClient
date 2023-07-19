

namespace NightWatchClientApp.Models;


public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public int? TeamId { get; set; }
    public bool IsPremium { get; set; }

    public User()
    {
        
    }

    public User(string email, string password, string fullName)
    {
        Email = email;
        Password = password;
        FullName = fullName;
        IsPremium = false;
        TeamId = null;
    }
}

